using System;
using System.Collections.Generic;

namespace AutoPay.Models.Response.Payment
{
    public class TransactionResponseModel
    {
        public string AuthCode { get; set; }
        public string TransactionId { get; set; }
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }
        public IEnumerable<TransactionErrorModel> Errors { get; set; }
    }
}
