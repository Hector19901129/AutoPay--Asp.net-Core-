using System.Collections.Generic;

namespace AutoPay.ViewModels.Batch
{
    public class BatchVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BatchCustomerVm> Customers { get; set; }
    }
}
