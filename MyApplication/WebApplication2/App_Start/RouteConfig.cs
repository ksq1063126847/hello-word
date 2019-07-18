﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:null,
                url:"",
                defaults: new { controller = "Product",action = "List" ,category=(string)null,page=1}
                );
            routes.MapRoute(
                name:null,
                url:"Page{page}",//更改路由模式 （例子: Index?1 变成 Index1）
                defaults: new { controller = "Product",action="List" , category = (string)null },
                constraints: new { page =@"\d+"}
                );
            routes.MapRoute(
               name: null,
               url: "{category}",
               defaults: new { controller = "Product", action = "List", page=1 }
               );
            routes.MapRoute(
               name: null,
               url: "{category}/Page{page}",
               defaults: new { controller = "Product", action = "List"},
               constraints: new { page = @"\d+" }
               );
            routes.MapRoute(
             name: null,
             url: "{controller}/{action}"           
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
