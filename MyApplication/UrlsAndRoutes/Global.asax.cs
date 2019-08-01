using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //1.区域 (遍历整个solution，查找AreaRegistration子类，划分区域。)
            AreaRegistration.RegisterAllAreas();
            //2.路由
            RouteConfig.RegisterRoutes(RouteTable.Routes); 
        }
    }
}
