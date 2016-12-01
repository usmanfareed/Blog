using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Controllers
{
    public class HomePostsController : Controller
    {
        // GET: HomePosts
        public ActionResult Index()
        {
            return View();
        }
    }
}