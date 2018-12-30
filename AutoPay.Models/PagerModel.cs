namespace AutoPay.Models
{
    public class PagerModel
    {
        public string FilterKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int GetRecordsToSkip()
        {
            return PageSize * (Page == 0 ? 0 : Page - 1);
        }
    }
}
