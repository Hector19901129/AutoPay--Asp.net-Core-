namespace AutoPay.Dtos.Customer
{
    public class CustomerDetailDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string ZipCode { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Ccv { get; set; }
    }
}
