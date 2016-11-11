using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Areas.AdminPanel.ViewModels;
using Blog.Models;

namespace Blog.Areas.AdminPanel.Controllers
{
    [RouteArea("AdminPanel")]
    [RoutePrefix("Posts")]
    public class PostsController : Controller
    {
        [Route("")]
        // GET: AdminPanel/Posts
        public ActionResult Index()
        {
            return View();
        }

        [Route("CreatePost")]
        public ActionResult CreatePost(Post post)
        {
            return Content("Success");
        }


        [Route("uploadnow")]

        public void UploadNow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string imageName = upload.FileName;
                string path = Path.Combine(Server.MapPath("~/Images/Uploads"), imageName);
                upload.SaveAs(path);

            }
        }


        [Route("uploadpartial")]
        public ActionResult UploadPartial()
        {
            var appData = Server.MapPath("~/Images/uploads");

            IEnumerable<ImagePostViewModel> images = Directory.GetFiles(appData).Select(x => new ImagePostViewModel()
            {
                Url = Url.Content("/Images/Uploads/" + Path.GetFileName(x))
            });

            return View(images);
        }



        [Route("deleteimg")]
        [HttpPost]
        public ActionResult DeleteImage(string url)
        {
            try
            {
                FileInfo image = new FileInfo(Server.MapPath(url));
                image.Delete();

                return Content("success");
            }
            catch (Exception)
            {
                
                throw;
            }

        }



    }
}