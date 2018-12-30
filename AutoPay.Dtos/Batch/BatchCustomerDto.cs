using System;

namespace AutoPay.Dtos.Batch
{
    public class BatchCustomerDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AmountDue { get; set; }
        public string IsExistsInLocalDb { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
