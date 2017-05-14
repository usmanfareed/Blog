using System.Linq;
using System.Web.Mvc;
using Blog.Interfaces.IRepository;
using Blog.Models;

namespace Blog.WebUI.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private readonly IRepositoryBase<Post> _repositoryBase;
        private readonly IPostRepository _postRepository;

        public HomeController(IRepositoryBase<Post> post,IPostRepository postRepository)
        {
            this._repositoryBase = post;
            this._postRepository = postRepository;
        }

        [Route("~/")]
        public ActionResult Index()
        {
            return View ("Index");
        }

        [Route("~/getposts")]
        [OutputCache(Duration = int.MaxValue, SqlDependency = "BlogDatabase:Posts", VaryByParam = "*")]
        public ActionResult GetPosts(int skipCount, int takeCount)
        {
           var posts = _postRepository.GetAllPosts().Skip(skipCount).Take(takeCount).ToList();
            if (posts.Any())
            {
                return PartialView("Index", posts);
            }
            return null;
        }


        [Route("~/{slug}")]

        public ActionResult Post(string slug)
        {
           var post= _postRepository.GetBySlug(slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            _postRepository.UpdateViewCount(post.Id);
            return View(post);
        }


        [Route("~/tag/{tag}")]
        [OutputCache(Duration = int.MaxValue, SqlDependency = "BlogDatabase:Posts", VaryByParam = "*")]

        public ActionResult Tag(string tag,int takeCount = 5, int skipCount = 0)
        {

            var posts = _postRepository.GetAllPostsByTag(tag).Skip(skipCount).Take(takeCount).ToList();
            ViewBag.tag = tag;

            if (Request.IsAjaxRequest())
            {
                if (posts.Any())
                {
                    return PartialView("TagPosts", posts);

                }
                return null;
            }

            return View("TagPosts", posts);

        }

        [Route("~/PartialTags")]
        public ActionResult PartialTags()
        {
            var tags = _postRepository.TopTags();
            return View("_Tags", tags);
        }


        [Route("~/search/{*search}")]
        public ActionResult SearchPosts(string search="")
        {
         var posts = _postRepository.SearchPosts(search);

            ViewBag.Search = search;
            return View("Index", posts);

        }
        [Route("~/Archive")]
        public ActionResult Archive()
        {
            var archive = _postRepository.LoadArchives();
            return View("Archives", archive);
            
        }

        [Route("~/AboutMe")]
        public ActionResult AboutMe()
        {
            return View();
        }

        [Route("~/ContactMe")]
        public ActionResult ContactMe()
        {
            return View();
        }
    }
}