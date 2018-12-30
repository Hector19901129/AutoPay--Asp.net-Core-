using System.Threading.Tasks;

namespace AutoPay.Infrastructure.Managers
{
    public interface IDataSeedManager
    {
        Task InitializeAsync();
    }
}
