using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Games",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Games", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Developers",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Developers", action = "Delete", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Genres",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Genres", action = "Delete", id = UrlParameter.Optional }
            );
        }
    }
}
