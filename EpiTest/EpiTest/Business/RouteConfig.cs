using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EpiTest.Business
{
    public static class RouteConfig
    {

        public static void MapRoutes()
        {            
            RouteTable.Routes.MapRoute(name: "RegisterRetailer", 
                url: "register/retailer",
                defaults: new { Controller = "RegisterRetailer", Action = "Index" });
        }
    }
}