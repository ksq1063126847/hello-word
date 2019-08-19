using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        //异常过滤器，实现IExceptionFilter接口
        //也可使用asp.net MVC 内置异常处理属性，HandleError
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == false && filterContext.Exception is ArgumentOutOfRangeException)
            {
                //1,使用静态页面响应异常
                //filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                //filterContext.ExceptionHandled = true;

                //2.使用视图响应异常
                int val = (int)((ArgumentOutOfRangeException)filterContext.Exception).ActualValue;
                filterContext.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary<int>(val)
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}