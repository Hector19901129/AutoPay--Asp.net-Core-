using System.ComponentModel.DataAnnotations;

namespace AutoPay.ViewModels.Account
{
    public class ChangePasswordVm
    {
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
