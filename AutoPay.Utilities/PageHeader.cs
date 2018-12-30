using System.Collections.Generic;

namespace AutoPay.Utilities
{
    public class PageHeader
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public IEnumerable<BreadcrumbItem> BreadcrumbItems { get; set; }
    }
}
