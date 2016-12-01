using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Blog.WebUI.App_Start;

namespace Blog.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c=>c.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


    }
}
