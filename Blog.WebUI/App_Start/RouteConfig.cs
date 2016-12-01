using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapMvcAttributeRoutes();
            AreaRegistration.RegisterAllAreas();

            //var namespaces = new[] { typeof(HomeController).Namespace };

            //routes.MapRoute("Default","{controller}/{action}/{id}",new { controller = "Home", action = "Index", id = UrlParameter.Optional  }, namespaces
            //);
        }
    }
}
