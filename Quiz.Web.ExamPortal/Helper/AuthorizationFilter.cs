using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.ExamPortal.Helper
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["userid"] == null)
            {
                string logoutURL = ConfigurationManager.AppSettings["WebUIUrl"];
                filterContext.Result = new RedirectResult(logoutURL);
                return;
            }
            else if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string logoutURL = ConfigurationManager.AppSettings["WebUIUrl"];
                filterContext.Result = new RedirectResult(logoutURL);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}