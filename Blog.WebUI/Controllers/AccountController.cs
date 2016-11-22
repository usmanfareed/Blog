using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Route("~/auth")]
        public ActionResult Index()
        {
            return View();
        }



    

    }
}