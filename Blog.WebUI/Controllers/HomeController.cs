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

        public ActionResult Tag(string tag)
        {
            var posts = _postRepository.GetAllPostsByTag(tag);
            return View("Index", posts);
        }

        [Route("~/PartialTags")]
        public ActionResult PartialTags()
        {
            var tags = _postRepository.TopTags();
            return View("_Tags", tags);
        }


        [Route("~/search/{search}")]
        public ActionResult SearchPosts(string search)
        {
            var posts = _postRepository.SearchPosts(search);
            return View("Index", posts);
        }



    }
}