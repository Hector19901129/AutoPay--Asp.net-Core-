using System.Collections.Generic;

namespace AutoPay.Entities
{
    public class BatchCustomer
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AmountDue { get; set; }
        public string IsExistsInLocalDb { get; set; }
        public string PaymentStatus { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<BatchCustomerDueDetail> DueDetails { get; set; }
    }
}
