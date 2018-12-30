using System;

namespace AutoPay.Dtos.Payment
{
    public class PaymentErrorDto
    {
        public DateTime TransactionDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
