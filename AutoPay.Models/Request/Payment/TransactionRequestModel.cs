using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPay.Models.Request.Payment
{
    public class TransactionRequestModel
    {
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string Ccv { get; set; }
        public decimal Amount { get; set; }
    }
}
