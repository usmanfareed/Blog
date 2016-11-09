using System.Data.Entity;

namespace Blog.Models
{
    public class BlogDbContext:DbContext

    
    {

        //public BlogDbContext():base("BlogDatabase")
        //{
            
        //}


        public DbSet<Comment>Comment { get; set; }
        public DbSet<Post>Post { get; set; }
        public DbSet<Tag>Tag { get; set; }
    }
}