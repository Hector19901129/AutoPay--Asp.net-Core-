using System;
using System.Collections.Generic;
using AutoPay.Utilities;

namespace AutoPay.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string SqlQuery { get; set; }
        public int CustomersCount { get; set; }
        public BatchStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<BatchCustomer> Customers { get; set; }
    }
}
