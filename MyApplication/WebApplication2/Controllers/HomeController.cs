using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ServicesClass;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        IHelper helper;
        public HomeController(IHelper helper)
        {
            this.helper = helper;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Test()
        {
            ViewBag.theName = "hello world!";
            return View();
        }

        [HttpPost]
        public ActionResult Test(TestEntity entity)
        {
            if (ModelState.IsValid)
                return View("Thanks", entity);
            else
                return View();
        }
    }
}