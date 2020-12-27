using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CnWeb_FastFood
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );/*.DataTokens.Add("area", "Admin");*/

            routes.MapRoute(
               name: "Admin",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
           ).DataTokens.Add("area", "Admin");

          //  routes.MapRoute(
          //    name: "Search",
          //    url: "Searching",
          //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
          //    namespaces: new[] { "CnWeb_FastFood.Controllers" }
          //);

            routes.MapRoute(
       name: "Search",
       url: "{controller}/{action}/{id}",
       defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
       namespaces: new[] { "CnWeb_FastFood.Controllers" }
   );


            routes.MapRoute(
               name: "Add Cart",
               url: "Them-gio-hang",
               defaults: new { controller = "ShopCart", action = "AddItem", id = UrlParameter.Optional },
               namespaces: new[] {"CnWeb_FastFood.Controllers"}
           );
        }
    }
}
