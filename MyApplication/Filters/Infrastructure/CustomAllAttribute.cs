using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class CustomAllAttribute : ActionFilterAttribute
    {
        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        public virtual void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }

        public virtual void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }
    }
}