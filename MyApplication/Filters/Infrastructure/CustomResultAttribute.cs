using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class CustomResultAttribute : FilterAttribute, IResultFilter
    {
        private Stopwatch timer;
        //ActionResult被执行前
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }
        //ActionResult被执行厚
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write(string.Format("<div>Result elapsed time :{0:F6}</div>", timer.Elapsed.TotalSeconds));
        }     
    }
}