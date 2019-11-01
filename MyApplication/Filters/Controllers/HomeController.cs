using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public string Index()
        {
            return "This is the Index action on the Home Controller";
        }

        [GoogleAuth]
        [CustomAuth(true)]
        public string List()
        {
            return "This is the List action on the Home Controller";
        }

        //[RangeException]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public string RangeTest(int id)
        {
            if (id > 100)
                return string.Format("The id value is {0}", id);
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        [CustomAction] //动作过滤器（自定义）
        [CustomResult] //结果过滤器（自定义）
        [CustomAll] //实现了ActionFilter抽象类， 实现对 Action 和 ActionResult 的“双重”过滤
        public string FilterTest()
        {
            return "This is the FilterTest Action";
        }
    }
}