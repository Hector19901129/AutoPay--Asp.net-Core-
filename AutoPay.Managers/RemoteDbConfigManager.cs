using System.Linq;
using System.Threading.Tasks;
using AutoPay.Dtos.RemoteDbConfig;
using AutoPay.Entities;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;
using AutoPay.Infrastructure.Services;
using AutoPay.Utilities;
using AutoPay.ViewModels.RemoteDbConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AutoPay.Managers
{
    public class RemoteDbConfigManager : IRemoteDbConfigManager
    {
        private readonly HttpContext _httpContext;
        private readonly string _encryptionKey;
        private readonly ICryptographyService _cryptographyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<RemoteDbConfig> _repository;


        public RemoteDbConfigManager(IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache,
            ICryptographyService cryptographyService,
            IUnitOfWork unitOfWork,
            IRepository<RemoteDbConfig> repository)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _encryptionKey = cache.Get<string>(Utility.GetEncryptionKeyName(_httpContext.User.GetUserId()));
            _cryptographyService = cryptographyService;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task UpsertAsync(RemoteDbUpsertVm model)
        {
            var userId = _httpContext.User.GetUserId();

            var remoteDbConfig = await _repository.FindAsync(userId);

            if (remoteDbConfig != null)
            {
                remoteDbConfig.Server = model.Server;
                remoteDbConfig.Username = model.Username;
                remoteDbConfig.Password = model.Password;
                remoteDbConfig.Database = model.Database;
                remoteDbConfig.UpdateDueDetailSp = model.UpdateDueDetailSp;
                remoteDbConfig.GetDueDetailQuery = model.GetDueDetailQuery;

                _cryptographyService.Encrypt(remoteDbConfig, _encryptionKey, "UserId");

                _repository.Update(remoteDbConfig);
            }
            else
            {
                remoteDbConfig = new RemoteDbConfig
                {
                    Server = model.Server,
                    Username = model.Username,
                    Password = model.Password,
                    Database = model.Database,
                    UpdateDueDetailSp = model.UpdateDueDetailSp,
                    GetDueDetailQuery = model.GetDueDetailQuery,
                    UserId = userId
                };

                _cryptographyService.Encrypt(remoteDbConfig, _encryptionKey, "UserId");

                await _repository.InsertAsync(remoteDbConfig);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RemoteDbConfigDto> GetAsync()
        {
            var userId = _httpContext.User.GetUserId();
            var remoteDbConfig = await _repository.Filter(x => x.UserId.Equals(userId))
                .Select(x => new RemoteDbConfigDto
                {
                    Server = x.Server,
                    Username = x.Username,
                    Password = x.Password,
                    Database = x.Database,
                    UpdateDueDetailSp = x.UpdateDueDetailSp,
                    GetDueDetailQuery = x.GetDueDetailQuery

                }).SingleOrDefaultAsync();

            _cryptographyService.Decrypt(remoteDbConfig, _encryptionKey, "UserId");

            return remoteDbConfig;
        }
    }
}
