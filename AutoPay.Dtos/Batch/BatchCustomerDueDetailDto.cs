namespace AutoPay.Dtos.Batch
{
    public class BatchCustomerDueDetailDto
    {
        public string CustomerId { get; set; }
        public string RecType { get; set; }
        public string TransactionDate { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string AmountDue { get; set; }
        public string YearMonth { get; set; }
    }
}
