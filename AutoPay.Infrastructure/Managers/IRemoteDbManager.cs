using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPay.Dtos.Batch;
using AutoPay.Dtos.RemoteDbConfig;
using AutoPay.ViewModels.RemoteDbConfig;

namespace AutoPay.Infrastructure.Managers
{
    public interface IRemoteDbManager
    {
        Task<string> ValdateDatabaseDetailAsync(RemoteDbUpsertVm model);
        Task<IEnumerable<BatchCustomerDto>> GetCurrentChargesAsync(RemoteDbConfigDto model, string sqlQuery);
        Task UpdateCurrentChargesDetailAsync(RemoteDbConfigDto dbConfig);
        Task<IEnumerable<BatchCustomerDueDetailDto>> GetCurrentChargesDetailAsync(RemoteDbConfigDto dbConfig);
    }
}
