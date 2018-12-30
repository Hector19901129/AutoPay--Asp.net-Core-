using System;

namespace AutoPay.Dtos.Batch
{
    public class BatchCustomerDetailDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentAuthCode { get; set; }
        public string TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? IsSuccess { get; set; }
        public string PaymentError { get; set; }
    }
}
