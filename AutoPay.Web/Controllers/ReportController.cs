using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoPay.Infrastructure.Managers;
using AutoPay.ViewModels.Batch;

namespace AutoPay.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerManager _customerManager;
        private readonly IBatchManager _batchManager;

        public ReportController(IMapper mapper,
            ICustomerManager customerManager,
            IBatchManager batchManager)
        {
            _mapper = mapper;
            _customerManager = customerManager;
            _batchManager = batchManager;
        }

        public async Task<IActionResult> Customer()
        {
            return View(await _customerManager.GetAsync());
        }

        public async Task<IActionResult> Batch(int id)
        {
            var batch = await _batchManager.GetDetailAsync(id);
            var batchVm = _mapper.Map<BatchDetailVm>(batch);
            return View(batchVm);
        }
    }
}