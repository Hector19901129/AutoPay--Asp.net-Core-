using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoPay.Dtos.Batch;
using AutoPay.Dtos.Payment;
using AutoPay.Entities;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;
using AutoPay.Infrastructure.Services;
using AutoPay.Models.Request.Payment;
using AutoPay.Utilities;
using AutoPay.ViewModels.Batch;

namespace AutoPay.Managers
{
    public class BatchCustomerManager : IBatchCustomerManager
    {
        private readonly string _encryptionKey;
        private readonly string _userId;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BatchCustomer> _repository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<PaymentError> _paymentErrorRepository;
        private readonly IRepository<BatchCustomerDueDetail> _batchCustomerDueDetailRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IPaymentService _paymentService;

        public BatchCustomerManager(IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache,
            ILogger<BatchCustomerManager> logger,
            IUnitOfWork unitOfWork,
            IRepository<BatchCustomer> repository,
            IRepository<Customer> customerRepository,
            IRepository<Payment> paymentRepository,
            IRepository<PaymentError> paymentErrorRepository,
            IRepository<BatchCustomerDueDetail> batchCustomerDueDetailRepository,
            ICryptographyService cryptographyService,
            IPaymentService paymentService)
        {
            var httpContext = httpContextAccessor.HttpContext;
            _encryptionKey = cache.Get<string>(Utility.GetEncryptionKeyName(httpContext.User.GetUserId()));
            _userId = httpContext.User.GetUserId();
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _customerRepository = customerRepository;
            _paymentRepository = paymentRepository;
            _paymentErrorRepository = paymentErrorRepository;
            _batchCustomerDueDetailRepository = batchCustomerDueDetailRepository;
            _cryptographyService = cryptographyService;
            _paymentService = paymentService;
        }

        public async Task<BatchCustomerDto> GetAsync(string customerId, int batchId)
        {
            var customerIdHash = _cryptographyService.Encrypt(customerId, _encryptionKey);

            var batchCustomer = await (from bc in _repository.Entity()
                                       where bc.CustomerId.Equals(customerIdHash)
                                        && bc.BatchId == batchId
                                        && bc.Batch.UserId.Equals(_userId)
                                        && bc.Batch.Status != BatchStatus.Deleted
                                       select new BatchCustomerDto
                                       {
                                           CustomerId = bc.CustomerId,
                                           CustomerName = bc.CustomerName
                                       }).SingleOrDefaultAsync();

            _cryptographyService.Decrypt(batchCustomer, _encryptionKey, "Id");

            return batchCustomer;
        }

        public async Task CaptureChargeAsync(int batchCustomerId)
        {
            var batchCustomer = await _repository.Filter(x => x.Id == batchCustomerId, "Payments").SingleAsync();

            var customer = await _customerRepository.Entity().SingleAsync(x => x.Code.Equals(batchCustomer.CustomerId));

            _cryptographyService.Decrypt(batchCustomer, _encryptionKey, "Id", "BatchId", "Batch", "Payments");

            _cryptographyService.Decrypt(customer, _encryptionKey, "Id", "CreatedBy");

            _logger.LogInformation("Payment initiated for customer - " + batchCustomer.CustomerId);

            var transactionRequestModel = new TransactionRequestModel
            {
                CardNumber = "11111",//customer.CardNumber,
                CardExpiration = $"{Convert.ToInt32(customer.ExpiryMonth):00}{customer.ExpiryYear.Substring(2, 2)}",
                Ccv = customer.Ccv,
                Amount = Convert.ToDecimal(batchCustomer.AmountDue)
            };

            _logger.LogInformation(
                $"Transaction request model for customer {batchCustomer.CustomerId} \n{JsonConvert.SerializeObject(transactionRequestModel)}");

            var transactionResponseModel = _paymentService.MakePayment(transactionRequestModel);

            _logger.LogInformation(
                $"Transaction response model for customer {batchCustomer.CustomerId} \n{JsonConvert.SerializeObject(transactionResponseModel)}");

            var payment = new Payment
            {
                AuthCode = transactionResponseModel.AuthCode,
                TransactionId = transactionResponseModel.TransactionId,
                IsSuccess = transactionResponseModel.IsSuccess,
                CreatedOn = Utility.GetDateTime(),
                Errors = transactionResponseModel.Errors?.Select(x => new PaymentError
                {
                    Code = x.Code,
                    Description = x.Description
                }).ToList()
            };

            if (payment.Errors == null)
            {
                payment.Errors = new List<PaymentError>();
            }

            if (transactionResponseModel.Exception != null)
            {
                var errorMessage = transactionResponseModel.Exception.Message.Length < 512
                    ? transactionResponseModel.Exception.Message
                    : transactionResponseModel.Exception.Message.Substring(0, 512);

                payment.Errors.Add(new PaymentError
                {
                    Code = "500",
                    Description = errorMessage
                });
            }

            if (batchCustomer.Payments == null)
            {
                batchCustomer.Payments = new List<Payment>();
            }

            batchCustomer.Payments.Add(payment);

            batchCustomer.PaymentStatus = (payment.IsSuccess ? PaymentStatus.Completed : PaymentStatus.Failed).ToString();

            _cryptographyService.Encrypt(batchCustomer, _encryptionKey, "Id", "BatchId", "Batch", "Payments");

            _repository.Update(batchCustomer);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Payment completed for customer - " + batchCustomer.CustomerId);
        }

        public async Task<IEnumerable<PaymentErrorDto>> GetPaymentErrorsAsync(int id)
        {
            return await (from p in _paymentRepository.Entity()
                          where p.BatchCustomerId == id
                          join pe in _paymentErrorRepository.Entity()
                          on p.Id equals pe.PaymentId
                          select new PaymentErrorDto
                          {
                              TransactionDate = p.CreatedOn,
                              Code = pe.Code,
                              Description = pe.Description
                          }).ToListAsync();
        }

        public async Task UpdateAmountAsync(AmountDueEditVm model)
        {
            var batchCustomer = await _repository.Filter(x => x.Id == model.Id, "DueDetails").SingleAsync();

            batchCustomer.AmountDue =
                _cryptographyService.Encrypt(model.AmountDue.ToString(CultureInfo.InvariantCulture), _encryptionKey);

            var recType2Hash = _cryptographyService.Encrypt("2", _encryptionKey);

            var dueDetail = batchCustomer.DueDetails.SingleOrDefault(x => x.RecType.Equals(recType2Hash));
            if (dueDetail != null)
            {
                dueDetail.AmountDue = batchCustomer.AmountDue;
            }

            _repository.Update(batchCustomer);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BatchCustomerDueDetailListItemDto>> GetDueAmountDetailAsync(int batchCustomerId)
        {
            var dueDetails = await _batchCustomerDueDetailRepository.Entity().Where(x => x.BatchCustomerId == batchCustomerId)
                .Select(x => new BatchCustomerDueDetailListItemDto
                {
                    RecType = x.RecType,
                    AmountDue = x.AmountDue,
                    Description = x.Description
                }).ToListAsync();

            foreach (var dueDetail in dueDetails)
            {
                _cryptographyService.Decrypt(dueDetail, _encryptionKey);
            }

            return dueDetails;
        }
    }
}
