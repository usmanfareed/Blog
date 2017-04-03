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
            var posts = _repositoryBase.GetAll().ToList();
            return View ("Index",posts);
        }

        [Route("~/{slug}")]
        public ActionResult Post(string slug)
        {
           var post= _postRepository.GetBySlug(slug);
            return View(post);
        }
    }
}