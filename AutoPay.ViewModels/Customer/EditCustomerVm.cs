using System.ComponentModel.DataAnnotations;

namespace AutoPay.ViewModels.Customer
{
    public class EditCustomerVm
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        public int? CountryId { get; set; }
        [StringLength(10, MinimumLength = 5)]
        public string ZipCode { get; set; }
        [Required]
        public int CardType { get; set; }
        [Required]
        [CreditCard]
        [StringLength(16, MinimumLength = 15)]
        public string CardNumber { get; set; }
        [Required]
        public int ExpiryMonth { get; set; }
        [Required]
        public int ExpiryYear { get; set; }
        [StringLength(4, MinimumLength = 3)]
        public string Ccv { get; set; }
    }
}
