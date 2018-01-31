using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Web.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
             
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "hacienda/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Adding formatter for Json   
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            // config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
        }
    }
}