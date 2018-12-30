using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPay.Dtos.Customer;
using AutoPay.ViewModels.Customer;

namespace AutoPay.Infrastructure.Managers
{
    public interface ICustomerManager
    {
        Task InsertAsync(AddCustomerVm model);
        Task<bool> IsExists(string code);
        Task<bool> IsExists(int id, string code);
        Task<IEnumerable<CustomerListItemDto>> GetAsync();
        Task<CustomerDetailDto> GetAsync(int id);
        Task<CustomerDetailDto> GetDetailAsync(string customerId);
        Task UpdateAsync(EditCustomerVm model);
        Task<bool> DeleteAsync(int id);
    }
}
