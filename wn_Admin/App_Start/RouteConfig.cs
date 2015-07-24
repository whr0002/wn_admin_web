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
                defaults: new { controller = "API", action = "PayPeriods", date = "" }

                );

            routes.MapRoute(
                name: "Supervision",
                url: "Supervisions/Edit/{eid}/{mid}/{pid}",
                defaults: new { controller = "Supervisions", action = "Edit", eid = "", mid = "", pid = "" }

                );

            routes.MapRoute(
                name: "Supervision delete",
                url: "Supervisions/Delete/{eid}/{mid}/{pid}",
                defaults: new { controller = "Supervisions", action = "Delete", eid = "", mid = "", pid = "" }
                );

            routes.MapRoute(
                name: "Supervision delete confirm",
                url: "Supervisions/DeleteConfirmed/{eid}/{mid}/{pid}",
                defaults: new { controller = "Supervisions", action = "Delete", eid = "", mid = "", pid = "" }
                );

            routes.MapRoute(
                name: "Supervision detail",
                url: "Supervisions/Detail/{eid}/{mid}/{pid}",
                defaults: new { controller = "Supervisions", action = "Detail", eid = "", mid = "", pid = "" }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
