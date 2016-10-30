using System.Data.Entity;
using Blog.Models;

namespace Blog.Areas.BlogSite.Models
{
    public class BlogDbContext:DbContext
    {
        public DbSet<BlogComment>Comment { get; set; }
        public DbSet<BlogPost>Post { get; set; }
        public DbSet<BlogTag>Tag { get; set; }
    }
}