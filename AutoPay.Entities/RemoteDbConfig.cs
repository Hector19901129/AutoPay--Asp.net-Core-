namespace AutoPay.Entities
{
    public class RemoteDbConfig
    {
        public string UserId { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string UpdateDueDetailSp { get; set; }
        public string GetDueDetailQuery { get; set; }
    }
}
