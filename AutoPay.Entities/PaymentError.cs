namespace AutoPay.Entities
{
    public class PaymentError
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
