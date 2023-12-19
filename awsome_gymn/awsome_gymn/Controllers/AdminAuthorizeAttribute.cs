using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace awsome_gymn.Controllers
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // User is not logged in, redirect to login page
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            else if (!filterContext.HttpContext.User.IsInRole("admin"))
            {
                // User is not in the "admin" role, redirect to access denied page
                filterContext.Result = new RedirectResult("~/Classes/AccessDenied");
            }
        }
    }
}