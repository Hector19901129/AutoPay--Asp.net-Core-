using System.Collections.Generic;

namespace AutoPay.DataLayer
{
    public class MasterData
    {
        public static IEnumerable<MasterDataItem> CardTypes =>
            new List<MasterDataItem>
        {
            new MasterDataItem {Id = 1, Name = "Visa"},
            new MasterDataItem {Id = 2, Name = "Mastercard"},
            new MasterDataItem {Id = 3, Name = "Express"},
            new MasterDataItem {Id = 4, Name = "Discover"},
            new MasterDataItem {Id = 5, Name = "JCB"}
        };

        public static IEnumerable<MasterDataItem> CardExpiryMonths =>
            new List<MasterDataItem>
            {
                new MasterDataItem {Id=1, Name = "Jan (01)"},
                new MasterDataItem {Id=2, Name = "Feb (02)"},
                new MasterDataItem {Id=3, Name = "Mar (03)"},
                new MasterDataItem {Id=4, Name = "Apr (04)"},
                new MasterDataItem {Id=5, Name = "May (05)"},
                new MasterDataItem {Id=6, Name = "Jun (06)"},
                new MasterDataItem {Id=7, Name = "Jul (07)"},
                new MasterDataItem {Id=8, Name = "Aug (08)"},
                new MasterDataItem {Id=9, Name = "Sep (09)"},
                new MasterDataItem {Id=10, Name = "Oct (10)"},
                new MasterDataItem {Id=11, Name = "Nov (11)"},
                new MasterDataItem {Id=12, Name = "Dec (12)"},
            };
    }
}
