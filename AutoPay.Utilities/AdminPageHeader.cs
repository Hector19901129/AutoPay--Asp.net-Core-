namespace AutoPay.Utilities
{
    public class AdminPageHeader
    {
        public static PageHeader GetDashboardHeader()
        {
            return new PageHeader
            {
                Title = "Dashboard",
                BreadcrumbItems = Breadcrumb.GetDashboardItems()
            };
        }

        public static PageHeader GetChangePasswordHeader()
        {
            return new PageHeader
            {
                Title = "Change Password",
                BreadcrumbItems = Breadcrumb.GetChangePasswordItems()
            };
        }

        public static PageHeader GetRemoteDbConfigHeader()
        {
            return new PageHeader
            {
                Title = "Remote DB Configuration",
                BreadcrumbItems = Breadcrumb.GetRemoteDbConfigItems()
            };
        }

        public static PageHeader GetManageBatchesHeader()
        {
            return new PageHeader
            {
                Title = "Batches",
                BreadcrumbItems = Breadcrumb.GetManageBatchesItems()
            };
        }

        public static PageHeader GetProcessBatchHeader()
        {
            return new PageHeader
            {
                Title = "Process Batch",
                BreadcrumbItems = Breadcrumb.GetBatchProcessItems()
            };
        }

        public static PageHeader GetBatchDetailHeader()
        {
            return new PageHeader
            {
                Title = "Batch Detail",
                BreadcrumbItems = Breadcrumb.GetBatchDetailItems()
            };
        }

        public static PageHeader GetBatchPaymentErrorHeader(int batchId)
        {
            return new PageHeader
            {
                Title = "Payment Errors",
                BreadcrumbItems = Breadcrumb.GetBatchPaymentErrorItems(batchId)
            };
        }
        public static PageHeader GetCurrentChargeHeader()
        {
            return new PageHeader
            {
                Title = "Current Charges",
                BreadcrumbItems = Breadcrumb.GetCurrentChargeItems()
            };
        }

        public static PageHeader GetManageCustomersHeader()
        {
            return new PageHeader
            {
                Title = "Customers",
                BreadcrumbItems = Breadcrumb.GetManageCustomersItems()
            };
        }

        public static PageHeader GetAddCustomerHeader()
        {
            return new PageHeader
            {
                Title = "Add Customer",
                BreadcrumbItems = Breadcrumb.GetAddCustomerItems()
            };
        }

        public static PageHeader GetEditCustomerHeader()
        {
            return new PageHeader
            {
                Title = "Edit Customer",
                BreadcrumbItems = Breadcrumb.GetEditCustomerItems()
            };
        }

        public static PageHeader GetCustomerDetailHeader()
        {
            return new PageHeader
            {
                Title = "Customer Detail",
                BreadcrumbItems = Breadcrumb.GetCustomerDetailItems()
            };
        }
    }
}
