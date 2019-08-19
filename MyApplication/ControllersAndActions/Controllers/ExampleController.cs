using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            return View();
        }

        public RedirectToRouteResult Redirect()
        {           
            return RedirectToAction("Index"); // 此方法是 RedirectToRoute() 的封装版本
        }

        public HttpStatusCodeResult StatusCode()
        {
            //1.发送404结果
            return HttpNotFound(); // 控制器提供的 404 便利方法 
            //return new HttpStatusCodeResult(404, "URL cannot be serviced");

            //2.发送401结果, 用来指示一个未授权的请求
            //return new HttpUnauthorizedResult();
        }
    }
}