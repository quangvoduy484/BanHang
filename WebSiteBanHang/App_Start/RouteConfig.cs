using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSiteBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //     name: "QuanLy",
            //     url: "QuanLy",
            //     defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }

            // );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Homepage", action = "Index", id = UrlParameter.Optional }
            );

         
        }
    }
}
