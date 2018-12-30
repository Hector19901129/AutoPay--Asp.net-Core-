using System.Threading.Tasks;
using AutoMapper;
using AutoPay.Infrastructure.Managers;
using AutoPay.Utilities;
using AutoPay.ViewModels.RemoteDbConfig;
using AutoPay.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AutoPay.Web.Controllers
{
    public class RemoteDbController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRemoteDbConfigManager _manager;
        private readonly IRemoteDbManager _remoteDbManager;

        public RemoteDbController(IMapper mapper,
            IRemoteDbConfigManager manager,
            IRemoteDbManager remoteDbManager)
        {
            _mapper = mapper;
            _manager = manager;
            _remoteDbManager = remoteDbManager;
        }

        public async Task<IActionResult> Index()
        {
            var remoteDbConfig = await _manager.GetAsync();
            var remoteDbUpsertVm = _mapper.Map<RemoteDbUpsertVm>(remoteDbConfig);
            return View(remoteDbUpsertVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm]RemoteDbUpsertVm model)
        {
            if (!ModelState.IsValid) return View(model);
            //validate database detail
            var result = await _remoteDbManager.ValdateDatabaseDetailAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                this.SetResponse(ResponseType.Error, result);
                return View(model);
            }
            //save details
            await _manager.UpsertAsync(model);
            this.SetResponse(ResponseType.Success, "Remote database configuration has been");
            return View(model);
        }
    }
}