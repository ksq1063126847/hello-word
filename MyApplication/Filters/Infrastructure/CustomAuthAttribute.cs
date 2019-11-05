using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Filters.Infrastructure
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        //授权过滤器
        private bool localAllowed;
        public CustomAuthAttribute(bool allowParam)
        {
            localAllowed = allowParam;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string name = httpContext.User.Identity.Name;
            //((FormsIdentity) httpContext.User.Identity ).Ticket;
            if (httpContext.User.Identity.IsAuthenticated)
                return true;
            else
                return false;

            //if (httpContext.Request.IsLocal)
            //{
            //    return localAllowed;
            //}
            //else
            //{
            //    return true;
            //}
        }
    }
}