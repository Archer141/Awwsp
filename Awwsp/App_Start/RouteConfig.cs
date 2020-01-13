using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Awwsp
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
            );

            routes.MapRoute(
              name: "Assign",
              url: "{controller}/{action}/{trophyId}/{ageGroupId}",
              defaults: new { controller = "Home", action = "Index"}
          );

        }
    }
    //public class BinaryIntellectViewEngine : RazorViewEngine
    //{
    //    public BinaryIntellectViewEngine()
    //    {
    //        string[] locations = new string[] {
    //        "~/Views/{1}/{0}.cshtml",
    //        "~/Views/Shared/Child/{0}.cshtml",
    //        "~/Views/Shared/Parent/{0}.cshtml",
    //        "~/Views/Shared/Manage/{0}.cshtml", 
    //        "~/Views/Shared/Navs/{0}.cshtml",
    //        "~/Views/Shared/Coach/{0}.cshtml",
    //        "~/Views/Shared/Layouts/{0}.cshtml"
    //    };

    //        this.ViewLocationFormats = locations;
    //        this.PartialViewLocationFormats = locations;
    //        this.MasterLocationFormats = locations;
    //    }
    //}
}
