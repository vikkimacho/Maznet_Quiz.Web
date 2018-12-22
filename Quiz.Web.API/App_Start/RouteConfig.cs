using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Quiz.Web.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute("DefaultApiWithId", "Api/{controller}/{id}", new { id = UrlParameter.Optional }, new { id = @"\d+" });
            //routes.MapRoute("DefaultApiWithAction", "Api/{controller}/{action}");
            ////routes.MapRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            ////routes.MapRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });


            routes.MapRoute("DefaultApiWithId", "Api/{controller}/{id}", new { id = UrlParameter.Optional }, new { id = @"\d+" });
            routes.MapRoute("DefaultApiWithAction", "Api/{controller}/{action}");
            routes.MapRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Get) });
            routes.MapRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Get) });

        }
    }
}

