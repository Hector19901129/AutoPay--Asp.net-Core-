using System;

namespace AutoPay.ViewModels.Payment
{
    public class PaymentErrorVm
    {
        public DateTime TransactionDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
