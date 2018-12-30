using System.ComponentModel.DataAnnotations;

namespace AutoPay.ViewModels.Batch
{
    public class AmountDueEditVm
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 10000000)]
        public decimal AmountDue { get; set; }
    }
}
