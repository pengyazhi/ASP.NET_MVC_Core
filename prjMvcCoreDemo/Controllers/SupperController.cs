using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using prjMvcCoreDemo.Models;

namespace prjMvcCoreDemo.Controllers
{
    public class SupperController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //要在執行這個Action之前做的動作用OnActionExecuting
            base.OnActionExecuting(context);
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Login"
                }));
            }
        }
    }
}
