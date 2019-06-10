using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebSiteBanHang
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //default : type data is json 
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver =
             new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
