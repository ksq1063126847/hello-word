﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    //继承RouteBase，自定义路由
    //对输入URL进行路由
    public class LegacyRoute : RouteBase
    {
        private string[] urls;
        public LegacyRoute(params string[] targetUrls)
        {
            urls = targetUrls;
        }

        //1.处理 输入URL
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            string requestedURL = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (urls.Contains(requestedURL, StringComparer.OrdinalIgnoreCase))
            {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("legacyURL", requestedURL);
            }
            return result;
        }
        //2.处理 输出URL
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            if (values.ContainsKey("legacyURL") && urls.Contains((string)values["legacyURL"], StringComparer.OrdinalIgnoreCase))
            {
                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyURL"]).Substring(1));
            }
            return result;
        }
    }
}