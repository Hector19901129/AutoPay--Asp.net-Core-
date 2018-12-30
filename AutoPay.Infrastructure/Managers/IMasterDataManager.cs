using System.Collections.Generic;
using System.Threading.Tasks;
using AutoPay.Dtos;

namespace AutoPay.Infrastructure.Managers
{
    public interface IMasterDataManager
    {
        Task<IEnumerable<SelectListItemDto>> GetCountriesAsync();
        IEnumerable<SelectListItemDto> GetCardTypes();
        IEnumerable<SelectListItemDto> GetCardExpiryMonths();
        IEnumerable<SelectListItemDto> GetCardExpiryYears();
    }
}
