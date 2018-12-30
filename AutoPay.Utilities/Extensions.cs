using System.Security.Claims;
using System.Security.Principal;

namespace AutoPay.Utilities
{
    public static class Extensions
    {
        public static string GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string ApplyCcNumberMask(this string cardNumber)
        {
            return string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 15
                   ? cardNumber
                    : cardNumber.Length == 15
                  ? "XXXX-XXXXXX-X" + cardNumber.Substring(cardNumber.Length - 4, 4)
                    : "XXXX-XXXX-XXXX-" + cardNumber.Substring(cardNumber.Length - 4, 4);
        }

        public static string ApplyCcvMask(this string ccv)
        {
            return string.IsNullOrEmpty(ccv)
                ? ccv
                : ccv.Length == 4
                ? "XXXX"
                : "XXX";
        }
    }
}
