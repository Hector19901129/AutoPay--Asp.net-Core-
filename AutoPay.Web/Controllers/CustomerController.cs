using AutoMapper;
using AutoPay.Infrastructure.Managers;
using AutoPay.Utilities;
using AutoPay.ViewModels.Customer;
using AutoPay.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoPay.Web.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerManager _customerManager;
        private readonly IBatchCustomerManager _batchCustomerManager;

        public CustomerController(IMapper mapper,
            ICustomerManager customerManager,
            IBatchCustomerManager batchCustomerManager)
        {
            _mapper = mapper;
            _customerManager = customerManager;
            _batchCustomerManager = batchCustomerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            return View(await _customerManager.GetAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add(string id, int? bId)
        {
            if (string.IsNullOrEmpty(id) || !bId.HasValue)
            {
                return View();
            }

            var batchCustomer = await _batchCustomerManager.GetAsync(id, bId.Value);
            if (batchCustomer == null)
            {
                return View();
            }

            var addCustomerVm = _mapper.Map<AddCustomerVm>(batchCustomer);

            addCustomerVm.BatchId = bId;

            return View(addCustomerVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm]AddCustomerVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _customerManager.IsExists(model.Code))
            {
                this.SetResponse(ResponseType.Error, "Another customer with same code already exists in the database.", true);
                return View(model);
            }

            await _customerManager.InsertAsync(model);

            this.SetResponse(ResponseType.Success, "Customer has been added successfully.", true);

            return !model.BatchId.HasValue
                ? RedirectToAction("manage", "customer")
                : RedirectToAction("process", "batch", new { id = model.BatchId.Value });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerManager.GetAsync(id);
            var customerVm = _mapper.Map<EditCustomerVm>(customer);
            return View(customerVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]EditCustomerVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _customerManager.IsExists(model.Id, model.Code))
            {
                this.SetResponse(ResponseType.Error, "Another customer with same code already exists in the database.", true);
                return View(model);
            }

            await _customerManager.UpdateAsync(model);

            this.SetResponse(ResponseType.Success, "Customer has been updated successfully.", true);

            return RedirectToAction("manage", "customer");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id, int? bId)
        {
            var customer = await _customerManager.GetDetailAsync(id);
            var customerVm = _mapper.Map<CustomerDetailVm>(customer);

            if (bId.HasValue)
            {
                ViewBag.BatchId = bId;
            }
            return View(customerVm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _customerManager.DeleteAsync(id))
                this.SetResponse(ResponseType.Success, "Customer has been deleted successfully.", true);
            else
                this.SetResponse(ResponseType.Error, "This customer can not be deleted.", true);

            return RedirectToAction("manage", "customer");
        }
    }
}