using System.Web.Mvc;

namespace Blog.WebUI.Areas.AdminPanel.Controllers
{
    [RouteArea("AdminPanel")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}