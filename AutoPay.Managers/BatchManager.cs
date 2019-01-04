using AutoPay.Dtos.Batch;
using AutoPay.Entities;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;
using AutoPay.Infrastructure.Services;
using AutoPay.Utilities;
using AutoPay.ViewModels.Batch;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPay.Managers
{
    public class BatchManager : IBatchManager
    {
        private readonly HttpContext _httpContext;
        private readonly string _encryptionKey;
        private readonly string _userId;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Batch> _repository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly ICustomerManager _customerManager;

        public BatchManager(IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache,
            IUnitOfWork unitOfWork,
            IRepository<Batch> repository,
            IRepository<Customer> customerRepository,
            ICustomerManager customerManager,
            ICryptographyService cryptographyService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _userId = _httpContext.User.GetUserId();
            _encryptionKey = cache.Get<string>(Utility.GetEncryptionKeyName(_userId));
            _unitOfWork = unitOfWork;
            _repository = repository;
            _customerRepository = customerRepository;
            _cryptographyService = cryptographyService;
            _customerManager = customerManager;
        }

        public async Task<int> CreateAsync(BatchAddVm model, List<BatchCustomerDto> batchCustomers, List<BatchCustomerDueDetailDto> batchCustomerDueDetails)
        {
            var matchedCustomerCodes = (from bc in batchCustomers
                                        let customerIdHash = _cryptographyService.Encrypt(bc.CustomerId, _encryptionKey)
                                        join c in _customerRepository.Entity().Where(x => x.CreatedBy.Equals(_userId))
                                        on customerIdHash equals c.Code
                                        select bc.CustomerId).ToList();

            var batch = new Batch
            {
                UserId = _httpContext.User.GetUserId(),
                Name = model.Name,
                SqlQuery = "",
                CreatedOn = Utility.GetDateTime(),
                CustomersCount = batchCustomers.Count,
                Status = BatchStatus.Created,
                Customers = batchCustomers.Select(x => new BatchCustomer
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    AmountDue = x.AmountDue,
                    IsExistsInLocalDb = matchedCustomerCodes.Contains(x.CustomerId).ToString(),
                    PaymentStatus = PaymentStatus.NotInitiated.ToString(),
                    DueDetails = batchCustomerDueDetails.Where(y => y.CustomerId.Equals(x.CustomerId))
                        .Select(y => new BatchCustomerDueDetail
                        {
                            RecType = y.RecType,
                            TransactionDate = y.TransactionDate,
                            Reference = y.Reference,
                            Description = y.Description,
                            AmountDue = y.AmountDue,
                            YearMonth = y.YearMonth
                        }).ToList()
                }).ToList()
            };

            foreach (var customer in batch.Customers)
            {
                _cryptographyService.Encrypt(customer, _encryptionKey, "Id", "BatchId", "Batch", "Payments", "DueDetails");

                foreach (var dueDetail in customer.DueDetails)
                {
                    _cryptographyService.Encrypt(dueDetail, _encryptionKey, "Id", "BatchCustomerId");
                }
            }

            await _repository.InsertAsync(batch);
            await _unitOfWork.SaveChangesAsync();

            return batch.Id;
        }

        public async Task<IEnumerable<BatchListItemDto>> GetAsync()
        {
            return await (from b in _repository.Entity()
                          where b.UserId.Equals(_userId)
                          && b.Status != BatchStatus.Deleted
                          select new BatchListItemDto
                          {
                              Id = b.Id,
                              Name = b.Name,
                              SqlQuery = b.SqlQuery,
                              CustomersCount = b.CustomersCount,
                              Status = b.Status,
                              CreatedOn = b.CreatedOn,
                              UpdatedOn = b.UpdatedOn
                          }).ToListAsync();
        }

        public async Task<BatchDto> GetForProcessAsync(int id)
        {
            var customers = await _customerManager.GetAsync();
            List<string> codes = new List<string>();
            List<string> names = new List<string>();
            foreach(var customer in customers)
            {
                codes.Add(_cryptographyService.Encrypt(customer.Code, _encryptionKey));
                names.Add(_cryptographyService.Encrypt(customer.Name, _encryptionKey));
            }
            var completedPaymentStatusHash = _cryptographyService.Encrypt(PaymentStatus.Completed.ToString(), _encryptionKey);
            var batch = await (from b in _repository.Entity()
                               where b.Id == id
                               select new BatchDto
                               {
                                   Id = b.Id,
                                   Name = b.Name,
                                   Customers = from c in b.Customers
                                               where c.PaymentStatus != completedPaymentStatusHash && codes.Contains(c.CustomerId)
                                               let lastPayment = c.Payments.OrderByDescending(x => x.CreatedOn).FirstOrDefault()
                                               select new BatchCustomerDto
                                               {
                                                   Id = c.Id,
                                                   CustomerId = c.CustomerId,
                                                   CustomerName = names[codes.IndexOf(c.CustomerId)],
                                                   AmountDue = c.AmountDue,
                                                   IsExistsInLocalDb = c.IsExistsInLocalDb,
                                                   PaymentStatus = c.PaymentStatus,
                                                   TransactionDate = lastPayment.CreatedOn
                                               }
                               }).SingleOrDefaultAsync();

            /*if (batch?.Customers == null)
            {
                return batch;
            }*/

            batch.Customers = batch.Customers.ToList();

            foreach (var customer in batch.Customers)
            {
                _cryptographyService.Decrypt(customer, _encryptionKey, "Id", "TransactionDate");
            }

            batch.Customers = batch.Customers.OrderBy(x => x.CustomerId);

            return batch;
        }

        public async Task<BatchDetailDto> GetDetailAsync(int id)
        {
            var customers = await _customerManager.GetAsync();
            List<string> codes = new List<string>();
            List<string> names = new List<string>();
            foreach (var customer in customers)
            {
                codes.Add(_cryptographyService.Encrypt(customer.Code, _encryptionKey));
                names.Add(_cryptographyService.Encrypt(customer.Name, _encryptionKey));
            }
            var batch = await (from b in _repository.Entity()
                               where b.Id == id
                               select new BatchDetailDto
                               {
                                   Id = b.Id,
                                   Name = b.Name,
                                   Customers = from c in b.Customers
                                               where codes.Contains(c.CustomerId)
                                               let payment = c.Payments.OrderByDescending(x => x.CreatedOn).FirstOrDefault()
                                               select new BatchCustomerDetailDto
                                               {
                                                   Id = c.Id,
                                                   CustomerId = c.CustomerId,
                                                   CustomerName = names[codes.IndexOf(c.CustomerId)],
                                                   Amount = c.AmountDue,
                                                   PaymentStatus = c.PaymentStatus,
                                                   TransactionDate = payment.CreatedOn,
                                                   IsSuccess = payment.IsSuccess,
                                                   PaymentAuthCode = payment.AuthCode,
                                                   TransactionId = payment.TransactionId
                                               }
                               }).SingleOrDefaultAsync();

            /*if (batch?.Customers == null)
            {
                return batch;
            }*/

            batch.Customers = batch.Customers.ToList();

            foreach (var customer in batch.Customers)
            {
                _cryptographyService.Decrypt(customer, _encryptionKey, "Id", "TransactionDate", "IsSuccess", "PaymentAuthCode", "TransactionId");
            }

            return batch;
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            return await _repository.Entity()
                .AnyAsync(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) &&
                               x.UserId.Equals(_userId) && x.Status != BatchStatus.Deleted);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var batch = await _repository.FindAsync(id);
            if (batch.Status != BatchStatus.Created)
                return false;

            batch.Status = BatchStatus.Deleted;

            _repository.Update(batch);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task UpdateStatusAsync(int id)
        {
            var batch = await _repository.Filter(x => x.Id == id, "Customers").SingleAsync();

            var paymentStatusHash = _cryptographyService.Encrypt(PaymentStatus.Failed.ToString(), _encryptionKey);

            var batchStatus = batch.Customers.Any(x => x.PaymentStatus.Equals(paymentStatusHash))
                ? BatchStatus.Failed
                : BatchStatus.Completed;

            batch.Status = batchStatus;
            batch.UpdatedOn = Utility.GetDateTime();

            _repository.Update(batch);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}