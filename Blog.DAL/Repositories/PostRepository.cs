using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Data;
using Blog.Models;

namespace Blog.DAL.Repositories
{
    public class PostRepository : RepositoryBase<Post>
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
            return db.Post.Any(x => x.Title == title);
        }
    }
}
