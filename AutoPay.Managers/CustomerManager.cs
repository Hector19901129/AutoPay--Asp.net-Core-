using AutoPay.DataLayer;
using AutoPay.Dtos.Customer;
using AutoPay.Entities;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;
using AutoPay.Infrastructure.Services;
using AutoPay.Utilities;
using AutoPay.ViewModels.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace AutoPay.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly string _encryptionKey;
        private readonly string _userId;
        private readonly ICryptographyService _cryptographyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<BatchCustomer> _batchCustomerRepository;
        private readonly IRepository<Country> _countryRepository;

        public CustomerManager(IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache,
            ICryptographyService cryptographyService,
            IUnitOfWork unitOfWork,
            IRepository<Customer> repository,
            IRepository<BatchCustomer> batchCustomerRepository,
            IRepository<Country> countryRepository)
        {
            _cryptographyService = cryptographyService;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _batchCustomerRepository = batchCustomerRepository;
            _countryRepository = countryRepository;

            var httpContext = httpContextAccessor.HttpContext;
            _encryptionKey = cache.Get<string>(Utility.GetEncryptionKeyName(httpContext.User.GetUserId()));
            _userId = httpContext.User.GetUserId();
        }

        public async Task InsertAsync(AddCustomerVm model)
        {
            var customer = new Customer
            {
                Code = model.Code,
                Name = model.Name,
                Address = model.Address,
                City = model.City,
                State = model.State,
                CountryId = model.CountryId?.ToString(),
                ZipCode = model.ZipCode,
                CardNumber = model.CardNumber,
                CardType = model.CardType.ToString(),
                ExpiryMonth = model.ExpiryMonth.ToString(),
                ExpiryYear = model.ExpiryYear.ToString(),
                Ccv = model.Ccv,
                CreatedBy = _userId,
                CreatedOn = Utility.GetDateTime().ToString(CultureInfo.InvariantCulture),
                Status = RecordStatus.Active.ToString()
            };
            if(customer.Ccv == null)
            {
                _cryptographyService.Encrypt(customer, _encryptionKey, "Id", "CreatedBy", "Ccv");
            }
            else
            {
                _cryptographyService.Encrypt(customer, _encryptionKey, "Id", "CreatedBy");
            }
            

            var batchCustomer = await _batchCustomerRepository.Filter(x => x.Batch.UserId.Equals(_userId)
                && x.BatchId == model.BatchId
                && x.CustomerId.Equals(customer.Code)
                && x.Batch.Status != BatchStatus.Deleted, "Batch").SingleOrDefaultAsync();

            if (batchCustomer != null)
            {
                batchCustomer.IsExistsInLocalDb = _cryptographyService.Encrypt(true.ToString(), _encryptionKey);
                _batchCustomerRepository.Update(batchCustomer);
            }

            await _repository.InsertAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditCustomerVm model)
        {
            var customer = await _repository.FindAsync(model.Id);

            customer.Code = model.Code;
            customer.Name = model.Name;
            customer.Address = model.Address;
            customer.City = model.City;
            customer.State = model.State;
            customer.CountryId = model.CountryId?.ToString();
            customer.ZipCode = model.ZipCode;
            customer.CardNumber = model.CardNumber;
            customer.CardType = model.CardType.ToString();
            customer.ExpiryMonth = model.ExpiryMonth.ToString();
            customer.ExpiryYear = model.ExpiryYear.ToString();
            customer.Ccv = model.Ccv;
            customer.UpdatedOn = Utility.GetDateTime().ToString(CultureInfo.InvariantCulture);

            _cryptographyService.Encrypt(customer, _encryptionKey, "Id", "CreatedBy", "CreatedOn", "Status");

            _repository.Update(customer);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsExists(string code)
        {
            var encryptedCode = _cryptographyService.Encrypt(code, _encryptionKey);
            return await _repository.Entity()
                .AnyAsync(x => x.CreatedBy.Equals(_userId)
                    && x.Code.Equals(encryptedCode, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<bool> IsExists(int id, string code)
        {
            var encryptedCode = _cryptographyService.Encrypt(code, _encryptionKey);
            return await _repository.Entity()
                .AnyAsync(x => x.CreatedBy.Equals(_userId)
                    && x.Code.Equals(encryptedCode, StringComparison.InvariantCultureIgnoreCase)
                    && x.Id != id);
        }

        public async Task<IEnumerable<CustomerListItemDto>> GetAsync()
        {
            var statusHash = _cryptographyService.Encrypt(RecordStatus.Deleted.ToString(), _encryptionKey);

            var customers = await _repository.Entity()
                .Where(x => x.CreatedBy.Equals(_userId) && !x.Status.Equals(statusHash)).ToListAsync();

            _cryptographyService.Decrypt(customers, _encryptionKey, "Id", "CreatedBy");

            return (from c in customers
                    let countryId = Convert.ToInt32(c.CountryId)
                    //let expiryDate = Convert.ToDateTime($"30/04/2021")
                    let expiryDate = Convert.ToDateTime($"{DateTime.DaysInMonth(Convert.ToInt32(c.ExpiryYear), Convert.ToInt32(c.ExpiryMonth))}/{c.ExpiryMonth.PadLeft(2, '0')}/{c.ExpiryYear}")
                    let cardStatus = expiryDate < DateTime.Now ? CardStatus.Expired : expiryDate < DateTime.Now.AddMonths(3) ? CardStatus.Expring : CardStatus.Valid
                    join country in _countryRepository.Entity()
                    on countryId equals country.Id
                    select new CustomerListItemDto
                    {
                        Id = c.Id,
                        Code = c.Code,
                        Name = c.Name,
                        Address = c.Address,
                        City = c.City,
                        State = c.State,
                        Country = country.Name,
                        ZipCode = c.ZipCode,
                        CardNumber = c.CardNumber,
                        ExpiryMonth = c.ExpiryMonth,
                        ExpiryYear = c.ExpiryYear,
                        CardStatus = cardStatus
                    }).ToList();
        }

        public async Task<CustomerDetailDto> GetAsync(int id)
        {
            var customer = await _repository.Entity()
                .Where(x => x.Id == id && x.CreatedBy.Equals(_userId))
                .Select(x => new CustomerDetailDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    State = x.State,
                    ZipCode = x.ZipCode,
                    CountryId = x.CountryId,
                    CardType = x.CardType,
                    CardNumber = x.CardNumber,
                    ExpiryMonth = x.ExpiryMonth,
                    ExpiryYear = x.ExpiryYear,
                    Ccv = x.Ccv
                }).SingleOrDefaultAsync();

            if (customer == null)
            {
                return null;
            }

            _cryptographyService.Decrypt(customer, _encryptionKey, "Id");

            return customer;
        }

        public async Task<CustomerDetailDto> GetDetailAsync(string customerId)
        {
            var customerIdHash = _cryptographyService.Encrypt(customerId, _encryptionKey);

            var customer = await (from c in _repository.Entity()
                                  where c.Code.Equals(customerIdHash) && c.CreatedBy.Equals(_userId)
                                  select new CustomerDetailDto
                                  {
                                      Id = c.Id,
                                      Code = c.Code,
                                      Name = c.Name,
                                      Address = c.Address,
                                      City = c.City,
                                      State = c.State,
                                      ZipCode = c.ZipCode,
                                      CountryId = c.CountryId,
                                      CardType = c.CardType,
                                      CardNumber = c.CardNumber,
                                      ExpiryMonth = c.ExpiryMonth,
                                      ExpiryYear = c.ExpiryYear,
                                      Ccv = c.Ccv
                                  }).SingleOrDefaultAsync();

            if (customer == null)
            {
                return null;
            }

            _cryptographyService.Decrypt(customer, _encryptionKey, "Id");

            customer.CountryName = (await _countryRepository.Entity()
                .SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(customer.CountryId)))?.Name;

            customer.CardType = MasterData.CardTypes
                .SingleOrDefault(x => x.Id == Convert.ToInt16(customer.CardType))?.Name;

            customer.ExpiryMonth = MasterData.CardExpiryMonths
                .SingleOrDefault(x => x.Id == Convert.ToInt16(customer.ExpiryMonth))?.Name;

            return customer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _repository.FindAsync(id);
            if (await _batchCustomerRepository.Entity().AnyAsync(x => x.CustomerId.Equals(customer.Code)))
                return false;

            customer.Status = _cryptographyService.Encrypt(RecordStatus.Deleted.ToString(), _encryptionKey);

            _repository.Update(customer);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
