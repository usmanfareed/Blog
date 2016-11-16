using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Areas.AdminPanel.ViewModels;
using Blog.DAL.Data;
using Blog.DAL.Repositories;
using Blog.Models;

namespace Blog.Areas.AdminPanel.Controllers
{
    [RouteArea("AdminPanel")]
    [RoutePrefix("Posts")]
    public class PostsController : Controller
    {

        private readonly PostRepository _post = new PostRepository(new BlogDbContext());



        [Route("")]
        // GET: AdminPanel/Posts
        public ActionResult Index()
        {
            return View();
        }

        [Route("CreatePost")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(Post post)
        {
           
            _post.Insert(post);
            _post.Commit();
            return View("Index");
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