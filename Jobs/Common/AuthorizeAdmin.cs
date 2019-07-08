using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jobs.Common
{
    public class AuthorizeAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session == null || context.HttpContext.Session["Admin"] == null)
            {
                context.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Home", action = "Index", area = "" }));
            }
            base.OnActionExecuting(context);
        }
    }
}