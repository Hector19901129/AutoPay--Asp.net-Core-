using System.ComponentModel.DataAnnotations;

namespace AutoPay.ViewModels.Account
{
    public class LoginVm
    {
        [Required]
        [Display(Name = "Username")]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Encryption Key")]
        [StringLength(32, MinimumLength = 8)]
        public string Secret { get; set; }

        [Required]
        [Display(Name = "Remember? ")]
        public bool RememberPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
