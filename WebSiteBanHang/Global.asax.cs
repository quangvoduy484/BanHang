using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSiteBanHang.Models;

namespace WebSiteBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //DbMigrationsConfiguration.AutomaticMigrationsEnabled; 

          


          var context = new BanHangContext();
          var initializeMigrations = new MigrateDatabaseToLatestVersion<BanHangContext, Migrations.Configuration>();
          initializeMigrations.InitializeDatabase(context);
    }
    }
}
