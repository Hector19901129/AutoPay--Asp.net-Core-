using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoPay.DataLayer;
using Microsoft.EntityFrameworkCore;
using AutoPay.Dtos;
using AutoPay.Entities;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;

namespace AutoPay.Managers
{
    public class MasterDataManager : IMasterDataManager
    {
        private readonly IRepository<Country> _countryRepository;

        public MasterDataManager(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<SelectListItemDto>> GetCountriesAsync()
        {
            return await (from c in _countryRepository.Entity()
                          where c.Order == 0
                          orderby c.Name, c.Order
                          select new SelectListItemDto
                          {
                              Id = c.Id,
                              Name = c.Name
                          }).ToListAsync();
        }

        public IEnumerable<SelectListItemDto> GetCardTypes()
        {
            return MasterData.CardTypes.Select(x => new SelectListItemDto { Id = x.Id, Name = x.Name });
        }

        public IEnumerable<SelectListItemDto> GetCardExpiryMonths()
        {
            return MasterData.CardExpiryMonths.Select(x => new SelectListItemDto { Id = x.Id, Name = x.Name });
        }

        public IEnumerable<SelectListItemDto> GetCardExpiryYears()
        {
            var items = new List<SelectListItemDto>();
            for (var i = DateTime.Now.Year; i <= DateTime.Now.AddYears(20).Year; i++)
            {
                items.Add(new SelectListItemDto { Id = i, Name = i.ToString() });
            }
            return items;
        }
    }
}
