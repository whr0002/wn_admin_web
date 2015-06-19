using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace wn_Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Tasks",
            //    url: "Tasks/Edit/{id}",
            //    defaults: new {controller = "Tasks", action="Edit", id=UrlParameter.Optional}
            //    );

            routes.MapRoute(
                name: "Projects",
                url: "API/Projects/{client}",
                defaults: new { controller = "API", action = "Projects", client = "" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
