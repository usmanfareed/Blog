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
        [Route("")]
        public ActionResult Index()
        {
            var posts = _postRepository.GetAllPosts();
            return View ("Index",posts);
        }

        [Route("~/{slug}")]
        public ActionResult Post(string slug)
        {
           var post= _postRepository.GetBySlug(slug);
            _postRepository.UpdateViewCount(post.Id);
            return View(post);
        }
        [Route("~/tag/{tag}")]

        public ActionResult Tag(string tag)
        {
            var posts = _postRepository.GetAllPostsByTag(tag);
            return View("Index", posts);
        }

    }
}