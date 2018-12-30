using System.Threading.Tasks;
using AutoPay.Dtos.RemoteDbConfig;
using AutoPay.ViewModels.RemoteDbConfig;

namespace AutoPay.Infrastructure.Managers
{
    public interface IRemoteDbConfigManager
    {
        Task UpsertAsync(RemoteDbUpsertVm model);
        Task<RemoteDbConfigDto> GetAsync();
    }
}
