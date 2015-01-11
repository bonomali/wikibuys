using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace seoWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Blog",                                           // Route name
                "{id}",                            // URL with parameters
                new { controller = "User", action = "Details" }  // Parameter defaults
            );
            routes.MapRoute(
               "search",                                           // Route name
               "search/{id}",                            // URL with parameters
               new { controller = "Search", action = "Details" }  // Parameter defaults
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
