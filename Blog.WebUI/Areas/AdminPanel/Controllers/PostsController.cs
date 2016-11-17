using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
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



        [Route("create")]
        // GET: AdminPanel/Posts
        public ActionResult Index()
        {
            ViewBag.IsNew = true;
            return View();
        }

        [Route("CreatePost")]

        [ValidateInput(false),ValidateAntiForgeryToken]
        
        public ActionResult CreatePost(Post post)
        {

            if ( _post.IsExist(post.Title) )
            {
                ViewBag.title = "Fail";
                ViewBag.message = "Post of this title already exists";
               
                return View("Index");


            }

            _post.Insert(post);
            _post.Commit();
 
            return RedirectToAction("Posts");
        }



        [Route("")]

        public ActionResult Posts()
        {

           var posts = _post.GetAll();
            return View("Posts",posts);
        }

        // This action will delete post

        [Route("delete/{id}")]

        public ActionResult Delete(int id)
        {
           _post.Delete(id);
            _post.Commit();
           return RedirectToAction("Posts");
        }


        //This is action will change status
        [Route("status/{id}/{status}")]

        public ActionResult Status(int id,bool status)
        {

            var update = _post.GetById(id);
            update.IsActive = status;
           _post.Update(update);
            _post.Commit();
           return RedirectToAction("Posts");
        }


        // This action will edit post

        [Route("edit/{id}")]

        public ActionResult Edit(int id)
        {
            ViewBag.IsNew = false;
            var post = _post.GetById(id);
            return View("Index",post);
        }
         
        [Route("update")]
        [ValidateInput(false),ValidateAntiForgeryToken]
        public ActionResult Update(Post post)
        {
            //dont use find when using auto mapper
           //var updatePost = _post.GetById(post.Id);

           var updatePost =  new Post();
           updatePost = Mapper.Map(post, updatePost);
            _post.Update(updatePost);
            _post.Commit();
            return RedirectToAction("Posts");
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