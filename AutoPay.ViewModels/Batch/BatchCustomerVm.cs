using System;
using AutoPay.Utilities;

namespace AutoPay.ViewModels.Batch
{
    public class BatchCustomerVm
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal? AmountDue { get; set; }
        public bool IsExistsInLocalDb { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
