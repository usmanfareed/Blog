using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Areas.AdminPanel
{
    public class AdminPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            //context.Routes.MapMvcAttributeRoutes();
            //context.MapRoute(
            //    "AdminPanel_default",
            //    "AdminPanel/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}