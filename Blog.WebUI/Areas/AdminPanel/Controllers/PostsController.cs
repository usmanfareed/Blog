using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Blog.Areas.AdminPanel.ViewModels;
using Blog.DAL.Data;
using Blog.DAL.Repositories;
using Blog.Interfaces.IRepository;
using Blog.Models;
using Blog.WebUI.App_Start;
using Blog.WebUI.Areas.AdminPanel.ViewModels;


namespace Blog.WebUI.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    [RouteArea("AdminPanel")]
    [RoutePrefix("Posts")]
    public class PostsController : Controller
    {
        private readonly IRepositoryBase<Post> _repositoryBase;
        private readonly IRepositoryBase<Tag> _tagrepositoryBase;
        private readonly IPostRepository _postRepository;

        public PostsController(IRepositoryBase<Post> post,IPostRepository postRepository , IRepositoryBase<Tag> tagrepositoryBase)
        {
            this._repositoryBase = post;
            this._postRepository = postRepository;
            _tagrepositoryBase = tagrepositoryBase;
        }
 

        
             
        [Route("create")]
        // GET: AdminPanel/Posts
        public ActionResult Index()
        {
            var tags= _tagrepositoryBase.GetAll();
            ViewBag.tags=new MultiSelectList(tags,"Id","Title");
            ViewBag.IsNew = true;
            return View();
        }

        [Route("CreatePost")]

        [ValidateInput(false),ValidateAntiForgeryToken]
        
        public ActionResult CreatePost(PostIndexViewModel post)
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
            Post newPost = new Post{Content = post.Content,Title = post.Title,Slug = post.Slug};


            newPost.Tags = new List<Tag>();
            var alltags = _tagrepositoryBase.GetAll().ToList();
            List<Tag> list = new List<Tag>();
            foreach (var tag in post.SelectedTags)
            {
                
                if (!tag.IsInt())
                {
                    newPost.Tags.Add(new Tag() { Title = tag,Active = true,CreatedOn = DateTime.Now});
                }
                else
                {
                    var id = Convert.ToInt32(tag);
                    list.Add(alltags.Single(x => x.Id == id));
                }
            }
            newPost.Tags.AddRange(list);

            _postRepository.SavePost(newPost, list);
 
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
            _postRepository.UpdateStatus(id,status);
           return RedirectToAction("Posts");
        }


        // This action will edit post

        [Route("edit/{id}")]

        public ActionResult Edit(int id)
        {
            var tags = _tagrepositoryBase.GetAll().ToList();
            ViewBag.tags = new MultiSelectList(tags, "Id", "Title");
            ViewBag.IsNew = false;
            var post = _postRepository.GetById(id);
            var model = new PostIndexViewModel(){Tags = post.Tags,Content = post.Content,Slug = post.Slug,Id = post.Id,Title = post.Title};
            model.SelectedTags = tags.Where(x=>model.Tags.Any(y=>y.Id == x.Id)).Select(x => x.Id.ToString()).ToArray();
            return View("Index", model);
        }
         
        [Route("update")]
        [ValidateInput(false),ValidateAntiForgeryToken]
        public ActionResult Update(PostIndexViewModel post)
        {
            Post newPost = new Post {Id = post.Id,Content = post.Content, Title = post.Title, Slug = post.Slug};
            newPost.Tags = new List<Tag>();
            var alltags = _tagrepositoryBase.GetAll().ToList();
            List<Tag> list = new List<Tag>();
            foreach (var tag in post.SelectedTags)
            {

                if (!tag.IsInt())
                {
                    newPost.Tags.Add(new Tag() { Title = tag, Active = true, CreatedOn = DateTime.Now });
                }
                else
                {
                    var id = Convert.ToInt32(tag);
                    list.Add(alltags.Single(x => x.Id == id));
                }
            }
            newPost.Tags.AddRange(list);

            _postRepository.UpdatePost(newPost,list);

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