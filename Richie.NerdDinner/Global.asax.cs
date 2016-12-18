using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Richie.NerdDinner
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.ax/{*pathInfo}");
            //routes.MapRoute(
            //   name: "UpcomingDinners",
            //   url: "{controller}/{action}/{id}",
            //   defaults: new { controller = "DinnerController", action = "Index", id = "page" }
            //   );

        }
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
