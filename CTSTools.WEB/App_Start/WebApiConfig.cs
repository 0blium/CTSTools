using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CTSTools;


public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API routes
        config.MapHttpAttributeRoutes();
        config.Routes.MapHttpRoute(
            name: "Default",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        //odatamapper
        //config.AddODataQueryFilter();
        //ODataModelBuilder builder = new ODataConventionModelBuilder();
        ////builder.EntitySet<Letter_Header_ViewModel.Letter_HeaderDTO>("Letter_Header");


        //config.MapODataServiceRoute(
        // routeName: "odata",
        // routePrefix: "odata",
        // model: builder.GetEdmModel());
    }
}
