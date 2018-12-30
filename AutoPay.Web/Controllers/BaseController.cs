using AutoPay.Utilities;
using AutoPay.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace AutoPay.Web.Controllers
{
    [Authorize(Roles = UserType.User)]
    public class BaseController : Controller
    {
        protected string EncryptionKey { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //get user
            var user = context.HttpContext.User;
            //get cache service
            var cache = (IMemoryCache)HttpContext.RequestServices.GetService(typeof(IMemoryCache));
            //get encryption key
            if (!cache.TryGetValue(Utility.GetEncryptionKeyName(user.GetUserId()), out string encryptionKey))
            {
                this.SetResponse("Your session has been expired. Please login to continue.", true);
                context.Result = RedirectToAction("login", "account");
            }

            //set username to view bag
            ViewBag.Username = user.Identity.Name;
            base.OnActionExecuting(context);
        }
    }
}