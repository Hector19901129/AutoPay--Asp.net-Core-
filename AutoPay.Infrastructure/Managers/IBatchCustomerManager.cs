using AutoPay.Dtos.Batch;
using AutoPay.Dtos.Payment;
using AutoPay.ViewModels.Batch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoPay.Infrastructure.Managers
{
    public interface IBatchCustomerManager
    {
        Task<BatchCustomerDto> GetAsync(string customerId, int batchId);
        Task CaptureChargeAsync(int batchCustomerId);
        Task<IEnumerable<PaymentErrorDto>> GetPaymentErrorsAsync(int id);
        Task UpdateAmountAsync(AmountDueEditVm model);
        Task<IEnumerable<BatchCustomerDueDetailListItemDto>> GetDueAmountDetailAsync(int batchCustomerId);
    }
}
