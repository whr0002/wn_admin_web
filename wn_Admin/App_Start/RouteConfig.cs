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
                name: "PayPeroids",
                url: "API/PayPeriods/{date}",
                defaults: new { controller = "API", action = "PayPeriods", date = ""}

                );

            routes.MapRoute(
                name: "Supervision",
                url: "Supervisions/Edit/{eid}/{mid}",
                defaults: new { controller = "Supervisions", action = "Edit", eid="", mid=""}

                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
