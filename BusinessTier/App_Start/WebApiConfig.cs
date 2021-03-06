using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BusinessTier
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CustomerReg",
                routeTemplate: "api/{controller}/{fname}/{lname}"
            );
            config.Routes.MapHttpRoute(
                name: "DepositandWithdraw",
                routeTemplate: "api/{controller}/{UID}/{accID}/{amount}"
            );


            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
