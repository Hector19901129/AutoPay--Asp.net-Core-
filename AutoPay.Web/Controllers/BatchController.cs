using AutoMapper;
using AutoPay.Dtos.Batch;
using AutoPay.Infrastructure.Managers;
using AutoPay.Utilities;
using AutoPay.ViewModels.Batch;
using AutoPay.ViewModels.Payment;
using AutoPay.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoPay.Web.Controllers
{
    public class BatchController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBatchManager _batchManager;
        private readonly IBatchCustomerManager _batchCustomerManager;
        private readonly IRemoteDbManager _remoteDbManager;
        private readonly IRemoteDbConfigManager _remoteDbConfigManager;

        public BatchController(IMapper mapper,
            IBatchManager batchManager,
            IBatchCustomerManager batchCustomerManager,
            IRemoteDbManager remoteDbManager,
            IRemoteDbConfigManager remoteDbConfigManager)
        {
            _mapper = mapper;
            _batchManager = batchManager;
            _batchCustomerManager = batchCustomerManager;
            _remoteDbManager = remoteDbManager;
            _remoteDbConfigManager = remoteDbConfigManager;
        }

        public async Task<IActionResult> Manage()
        {
            var batches = await _batchManager.GetAsync();
            var batchesVm = _mapper.Map<IEnumerable<BatchListItemVm>>(batches);
            return View(batchesVm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm]BatchAddVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var remoteDbConfig = await _remoteDbConfigManager.GetAsync();
            if (remoteDbConfig == null
                    || string.IsNullOrWhiteSpace(remoteDbConfig.Server)
                    || string.IsNullOrWhiteSpace(remoteDbConfig.Username)
                    || string.IsNullOrWhiteSpace(remoteDbConfig.Password)
                    || string.IsNullOrWhiteSpace(remoteDbConfig.Database)
                    || string.IsNullOrWhiteSpace(remoteDbConfig.UpdateDueDetailSp)
                    || string.IsNullOrWhiteSpace(remoteDbConfig.GetDueDetailQuery))
            {
                this.SetResponse(ResponseType.Error, "Please update the remote database settings before continue.", true);
                return RedirectToAction("index", "remotedb");
            }

            if (await _batchManager.IsExistsAsync(model.Name))
            {
                this.SetResponse(ResponseType.Error, "A batch with same name already exists.");
                return View(model);
            }

            List<BatchCustomerDto> batchCustomers;
            string sql_query = "Select ClubAccountNumber as 'CustomerId', ClubAccountNumber as 'CustomerName',  TotalAmount as 'AmountDue' From tmp_john_member_due Where RecType = 2";
            try
            {
                //batchCustomers = (await _remoteDbManager.GetCurrentChargesAsync(remoteDbConfig, model.SqlQuery))?.ToList();
                batchCustomers = (await _remoteDbManager.GetCurrentChargesAsync(remoteDbConfig, sql_query))?.ToList();
            }
            catch (Exception ex)
            {
                this.SetResponse(ResponseType.Error, ex.Message);
                return View(model);
            }


            if (batchCustomers == null || !batchCustomers.Any())
            {
                this.SetResponse(ResponseType.Information, "No record found for given query.", true);
                return View(model);
            }

            /*try
            {
                await _remoteDbManager.UpdateCurrentChargesDetailAsync(remoteDbConfig);
            }
            catch (Exception ex)
            {
                this.SetResponse(ResponseType.Error, ex.Message);
                return View(model);
            }*/

            List<BatchCustomerDueDetailDto> batchCustomerDueDetails;

            try
            {
                batchCustomerDueDetails = (await _remoteDbManager.GetCurrentChargesDetailAsync(remoteDbConfig))?.ToList();
            }
            catch (Exception ex)
            {
                this.SetResponse(ResponseType.Error, ex.Message);
                return View(model);
            }

            var batchId = await _batchManager.CreateAsync(model, batchCustomers, batchCustomerDueDetails);

            return RedirectToAction("process", "batch", new { id = batchId });
        }

        public async Task<IActionResult> Process(int id)
        {
            var batch = await _batchManager.GetForProcessAsync(id);
            var batchVm = _mapper.Map<BatchVm>(batch);
            return View(batchVm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var batch = await _batchManager.GetDetailAsync(id);
            var batchVm = _mapper.Map<BatchDetailVm>(batch);
            return View(batchVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _batchManager.DeleteAsync(id))
            {
                this.SetResponse(ResponseType.Success, "Batch has been deleted successfully.", true);
            }
            else
            {
                this.SetResponse(ResponseType.Error, "This batch can not be deleted.", true);
            }

            return RedirectToAction("manage", "batch");
        }

        public async Task<IActionResult> Reopen(int id)
        {
            await _batchManager.UpdateStatusToCreatedAsync(id);

            this.SetResponse(ResponseType.Success, "Reopened the batch successfully!", true);

            return RedirectToAction("manage", "batch");
        }

        [HttpPost]
        public async Task<IActionResult> Charge(int id)
        {
            await _batchCustomerManager.CaptureChargeAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            await _batchManager.UpdateStatusAsync(id);

            this.SetResponse(ResponseType.Success, "Approved transactions have been processed successfully.", true);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> CloseBatch(int id)
        {
            await _batchManager.UpdateStatusToCompletedAsync(id);

            this.SetResponse(ResponseType.Success, "Completed the batch successfully!", true);

            return Ok();
        }

        

        [HttpGet]
        public async Task<IActionResult> PaymentError(int id, int bId)
        {
            ViewBag.BatchId = bId;
            var paymentErrors = await _batchCustomerManager.GetPaymentErrorsAsync(id);
            return View(_mapper.Map<IEnumerable<PaymentErrorVm>>(paymentErrors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAmountDueDetail(int id)
        {
            var dueDetails = await _batchCustomerManager.GetDueAmountDetailAsync(id);
            return Ok(_mapper.Map<IEnumerable<BatchCustomerDueDetailListItemVm>>(dueDetails));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAmountDue([FromForm]AmountDueEditVm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorList());
            }

            await _batchCustomerManager.UpdateAmountAsync(model);

            this.SetResponse(ResponseType.Success, "Amount has been updated successfully.", true);

            return Ok();
        }
    }
}