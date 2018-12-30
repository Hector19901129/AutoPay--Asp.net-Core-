using System;
using AutoPay.Utilities;

namespace AutoPay.ViewModels.Batch
{
    public class BatchCustomerDetailVm
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal? Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentAuthCode { get; set; }
        public string TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool IsSuccess { get; set; }
    }
}
