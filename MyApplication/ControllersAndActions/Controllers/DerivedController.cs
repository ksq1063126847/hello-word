using ControllersAndActions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        // GET: Derived
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController Index Method";
            return View("MyView");
        }

        public ActionResult ProductOutput()
        {
            if (Server.MachineName == "TINY")
            {
                return new CustomRedirectResult { Url = "/Basic/Index" };               
            }
            else
            {
                Response.Write("Controller:Derived,Action:ProductOutput");
                return null;
            }
        }

    }
}