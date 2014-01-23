using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movies
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MovieDisplay",
                url: "Movie/{id}",
                defaults: new { controller = "Movie", action = "Movies", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PersonDisplay",
                url: "Person/{id}",
                defaults: new { controller = "Person", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}