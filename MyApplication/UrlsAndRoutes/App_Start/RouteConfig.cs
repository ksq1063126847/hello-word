using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //启用路由特性，eg： [Route("~/Test")]
            //routes.MapMvcAttributeRoutes(); 

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.RouteExistingFiles = true; //使路由计算先于文件检查

            //自定义路由(保持旧网址的URL不变的情况下，使旧网址转到新网址。自定义路由做了这种“转换”)
            //routes.Add(new LegacyRoute("~/articles/Windows_3", "~/Test/Index"));// 带上 点 会失效？ "~/articles/Windows_3.1_Overview.html"
            
            //1.路由是按顺序匹配的，越具体的路由，要先定义
            //routes.MapRoute("", "Shop/{action}", new { controller = "Home" });
            //routes.MapRoute("", "X{controller}/{action}");

            //2.看此路由的“细节”
            Route myRoute = routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                //constraints: new {
                //    controller ="^H.*" ,
                //    action ="Index|About",
                //    httpMethod =new HttpMethodConstraint("GET","POST"),
                //    id =new CompoundRouteConstraint(
                //        new IRouteConstraint[] {
                //            new AlphaRouteConstraint(),
                //            new MinLengthRouteConstraint(6)
                //        })},
                constraints: null,
                namespaces: new[] { "UrlsAndRoutes.Controllers" } //添加到一条路由的命名空间具有同等优先级，不会先检查第一、再检查第二（不是按顺序检查的，而是会被同等对待）。优先检查列出的namespace，之后恢复正常。
            );
            //myRoute.DataTokens["UseNamespaceFallback"] = false; //在上述namespace找不到则不再寻找，用此设置

            //3.路由是按顺序匹配的，不“具体”的路由后定义
            //routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            //4.添加路由的第二种写法。使用routes.MapRoute() 更为简洁
            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("myRoute", myRoute);
        }
    }
}
