using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPay.Dtos.Batch;
using AutoPay.ViewModels.Batch;

namespace AutoPay.Infrastructure.Managers
{
    public interface IBatchManager
    {
        Task<int> CreateAsync(BatchAddVm model, List<BatchCustomerDto> batchCustomers,
            List<BatchCustomerDueDetailDto> batchCustomerDueDetails);
        Task<IEnumerable<BatchListItemDto>> GetAsync();
        Task<BatchDto> GetForProcessAsync(int id);
        Task<BatchDetailDto> GetDetailAsync(int id);
        Task<bool> IsExistsAsync(string name);
        Task<bool> DeleteAsync(int id);
        Task UpdateStatusAsync(int id);
    }
}
