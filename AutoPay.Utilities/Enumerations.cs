namespace AutoPay.Utilities
{
    public enum RecordStatus { Created, Active, Inactive, Deleted }

    public enum SortDirection { Ascending = 0, Descending = 1 }

    public enum BatchStatus { Created, Completed, Failed, Deleted }

    public enum PaymentStatus { NotInitiated, Completed, Failed }

    public enum CardType { Visa = 1, Mastercard = 2, Express = 3, Discover = 4, Jcb = 5 }

    public enum CardStatus { Valid, Expring, Expired }

    public struct ResponseType
    {
        public const string Success = "success";
        public const string Warning = "warning";
        public const string Information = "information";
        public const string Error = "error";
    }

    public struct UserType
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
