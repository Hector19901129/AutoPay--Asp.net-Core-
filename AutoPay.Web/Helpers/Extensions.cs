using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AutoPay.Web.Helpers
{
    public static class Extensions
    {
        public static void SetResponse(this Controller controller, string content, bool nextPage = false)
        {
            if (!nextPage)
                controller.ViewBag.Response = content;
            else
                controller.TempData["Response"] = content;
        }

        public static void SetResponse(this Controller controller, string responseType, string content, bool nextPage = false)
        {
            if (!nextPage)
                controller.ViewBag.Response = $"{responseType}|{content}";
            else
                controller.TempData["Response"] = $"{responseType}|{content}";
        }

        public static IEnumerable<string> GetErrorList(this ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage.Replace("'", "")));
        }

        public static string GetBaseUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
