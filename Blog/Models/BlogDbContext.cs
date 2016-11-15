using System.Data.Entity;
using Blog.Models;

namespace Blog.Models
{
    public class BlogDbContext:DbContext

    
    {


        public DbSet<Comment>Comment { get; set; }
        public DbSet<Post>Post { get; set; }
        public DbSet<Tag>Tag { get; set; }
    }
}