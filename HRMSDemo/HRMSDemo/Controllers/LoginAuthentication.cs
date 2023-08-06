using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HRMSDemo.Controllers
{
    /// <summary>
    /// LoginAuthentication
    /// </summary>
    /// <seealso cref="System.Web.Mvc.AuthorizeAttribute" />
    public class LoginAuthentication : AuthorizeAttribute
    {
        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    HttpContext.Current.Session.Clear();
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Login" } });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}