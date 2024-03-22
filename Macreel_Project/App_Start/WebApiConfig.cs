using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Macreel_Project
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            //config.Formatters.Add(new System.Net.Http.Formatting.FormDataMediaTypeFormatter());
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
