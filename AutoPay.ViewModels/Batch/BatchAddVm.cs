using System.ComponentModel.DataAnnotations;

namespace AutoPay.ViewModels.Batch
{
    public class BatchAddVm
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
