using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Movies
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "WEBapi/{controller}/{id}",
                defaults: new { controller = "WebApi", id = RouteParameter.Optional }
            );
        }
    }
}
