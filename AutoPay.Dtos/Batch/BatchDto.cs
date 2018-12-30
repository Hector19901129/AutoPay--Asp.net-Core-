using System.Collections.Generic;

namespace AutoPay.Dtos.Batch
{
    public class BatchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BatchCustomerDto> Customers { get; set; }
    }
}
