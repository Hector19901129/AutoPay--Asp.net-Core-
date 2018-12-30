using System;
using AutoPay.Utilities;

namespace AutoPay.ViewModels.Batch
{
    public class BatchListItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SqlQuery { get; set; }
        public int CustomersCount { get; set; }
        public BatchStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
