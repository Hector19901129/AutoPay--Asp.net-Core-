using AutoPay.Models.Request.Payment;
using AutoPay.Models.Response.Payment;

namespace AutoPay.Infrastructure.Services
{
    public interface IPaymentService
    {
        TransactionResponseModel MakePayment(TransactionRequestModel model);
    }
}
