using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("")]
        public ActionResult Index()
        {
            return View ();
        }
    }
}