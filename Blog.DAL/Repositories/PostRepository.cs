using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Data;
using Blog.Interfaces.IRepository;
using Blog.Models;

namespace Blog.DAL.Repositories
{
    public class PostRepository : RepositoryBase<Post>,IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {
           
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }

        private BlogDbContext db = new BlogDbContext();
        

        public bool IsExist(string title)
        {
            return db.Posts.Any(x => x.Title == title);
        }
       
    }
}
