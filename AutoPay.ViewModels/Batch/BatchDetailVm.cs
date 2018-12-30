using System.Collections.Generic;

namespace AutoPay.ViewModels.Batch
{
    public class BatchDetailVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BatchCustomerDetailVm> Customers { get; set; }
    }
}
