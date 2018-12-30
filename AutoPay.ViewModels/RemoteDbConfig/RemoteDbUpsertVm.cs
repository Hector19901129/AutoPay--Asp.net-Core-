using System.ComponentModel.DataAnnotations;

namespace AutoPay.ViewModels.RemoteDbConfig
{
    public class RemoteDbUpsertVm
    {
        [Required]
        [MaxLength(256)]
        public string Server { get; set; }

        [Required]
        [MaxLength(256)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        [MaxLength(256)]
        public string Database { get; set; }

        [Required]
        [MaxLength(256)]
        public string UpdateDueDetailSp { get; set; }

        [Required]
        [MaxLength(1000)]
        public string GetDueDetailQuery { get; set; }
    }
}
