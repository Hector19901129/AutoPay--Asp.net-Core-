using System.Collections.Generic;

namespace AutoPay.Utilities
{
    public class Breadcrumb
    {
        public static IEnumerable<BreadcrumbItem> GetDashboardItems()
        {
            return null;
        }

        public static IEnumerable<BreadcrumbItem> GetChangePasswordItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Change Password" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetRemoteDbConfigItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Manage" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetManageBatchesItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Manage Batches" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetBatchProcessItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Batches", Url = "/batch/manage" },
                new BreadcrumbItem { Title = "Process" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetBatchDetailItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Batches", Url = "/batch/manage" },
                new BreadcrumbItem { Title = "Detail" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetBatchPaymentErrorItems(int batchId)
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Batches", Url = "/batch/manage" },
                new BreadcrumbItem { Title = "Detail", Url = "/batch/detail/" + batchId },
                new BreadcrumbItem { Title = "Payment Errors" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetCurrentChargeItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Get Current Charges" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetManageCustomersItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Manage Customers" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetAddCustomerItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Add Customer" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetEditCustomerItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Edit Customer" }
            };
        }

        public static IEnumerable<BreadcrumbItem> GetCustomerDetailItems()
        {
            return new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Dashboard", Url = "/" },
                new BreadcrumbItem { Title = "Customer Detail" }
            };
        }
    }
}
