using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Filters.Infrastructure
{
    public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        //认证过滤器，OnAuthentication 在所有Filter之前运行， 
        //OnAuthenticationChallenge 的执行顺序，和 传给他的filterContext.Result有关，若为null，在动作方法返回ActionResult时才执行。
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var ident = filterContext.Principal.Identity;
            if (!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com"))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller","GoogleAccount"},
                    { "action","Login"},
                    { "returnUrl",filterContext.HttpContext.Request.RawUrl}
                });
            }
        }
    }
}