namespace AutoPay.Dtos.RemoteDbConfig
{
    public class RemoteDbConfigDto
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string UpdateDueDetailSp { get; set; }
        public string GetDueDetailQuery { get; set; }
    }
}
