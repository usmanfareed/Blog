using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Areas.AdminPanel.ViewModels;
using Blog.DAL.Data;
using Blog.DAL.Repositories;
using Blog.Interfaces.IRepository;
using Blog.Models;
using Blog.WebUI.App_Start;


namespace Blog.WebUI.Areas.AdminPanel.Controllers
{
    [RouteArea("AdminPanel")]
    [RoutePrefix("Posts")]
    public class PostsController : Controller
    {
        private readonly IRepositoryBase<Post> _repositoryBase;
        private readonly IPostRepository _postRepository;

        public PostsController(IRepositoryBase<Post> post,IPostRepository postRepository  )
        {
            this._repositoryBase = post;
            this._postRepository = postRepository;
        }
        //private readonly IPostRepository _postRepository = AutoFacConfig.Container.Resolve<IPostRepository>();
        //private readonly IRepositoryBase<Posts> _repositoryBase = AutoFacConfig.Container.Resolve<IRepositoryBase<Posts>>();

        //_repositoryBase = new PostRepository(new BlogDbContext());
         //IRepositoryBase<Posts> _repositoryBase = new PostRepository(new BlogDbContext());

        
             
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
            if (!ModelState.IsValid)
            {
                return View("Index",post);
            }

            if ( _postRepository.IsExist(post.Title) )
            {
                ViewBag.title = "Fail";
                ViewBag.message = "Posts of this title already exists";
               
                return View("Index");


            }

            _repositoryBase.Insert(post);
            _repositoryBase.Commit();
 
            return RedirectToAction("Posts");
        }



        [Route("")]

        public ActionResult Posts()
        {

           var posts = _repositoryBase.GetAll();
            return View("Posts",posts);
        }

        // This action will delete post

        [Route("delete/{id}")]

        public ActionResult Delete(int id)
        {
           _repositoryBase.Delete(id);
            _repositoryBase.Commit();
           return RedirectToAction("Posts");
        }


        //This is action will change status
        [Route("status/{id}/{status}")]

        public ActionResult Status(int id,bool status)
        {

            var update = _repositoryBase.GetById(id);
            update.IsActive = status;
           _repositoryBase.Update(update);
            _repositoryBase.Commit();
           return RedirectToAction("Posts");
        }


        // This action will edit post

        [Route("edit/{id}")]

        public ActionResult Edit(int id)
        {
            ViewBag.IsNew = false;
            var post = _repositoryBase.GetById(id);
            return View("Index",post);
        }
         
        [Route("update")]
        [ValidateInput(false),ValidateAntiForgeryToken]
        public ActionResult Update(Post post)
        {
            //dont use find when using auto mapper
           //var updatePost = _repositoryBase.GetById(post.Id);

           //var updatePost =  new Posts();
           //updatePost = Mapper.Map(post, updatePost);
            _repositoryBase.Update(post);
            _repositoryBase.Commit();
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