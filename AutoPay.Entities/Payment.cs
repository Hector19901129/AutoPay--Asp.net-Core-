using System;
using System.Collections.Generic;

namespace AutoPay.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int BatchCustomerId { get; set; }
        public string AuthCode { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSuccess { get; set; }
        public virtual ICollection<PaymentError> Errors { get; set; }
    }
}
