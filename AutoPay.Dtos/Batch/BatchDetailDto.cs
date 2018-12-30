using System.Collections.Generic;

namespace AutoPay.Dtos.Batch
{
   public class BatchDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BatchCustomerDetailDto> Customers { get; set; }
    }
}
