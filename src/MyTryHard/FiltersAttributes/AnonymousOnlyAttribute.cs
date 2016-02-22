using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace MyTryHard.FiltersAttributes
{
    public class AnonymousOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToActionResult("index", "home", null);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
