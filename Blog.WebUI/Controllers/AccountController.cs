using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Route("~/auth/login")]
        public ActionResult Index()
        {
            return View();
        }



    

    }
}