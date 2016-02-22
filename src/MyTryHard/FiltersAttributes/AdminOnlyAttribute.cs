using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using System;

namespace MyTryHard.FiltersAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AdminOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (!user.IsInRole("Admin"))
            {
                filterContext.Result = new RedirectToActionResult("index", "home", null);
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
