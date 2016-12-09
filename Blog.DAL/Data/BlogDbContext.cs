using System.Data.Entity;
using Blog.Models;

namespace Blog.DAL.Data
{
    public class BlogDbContext:DbContext

    
    {
        public BlogDbContext()
            :base("BlogDatabase")
        {

        }

        public DbSet<Comment>Comments { get; set; }
        public DbSet<Post>Posts { get; set; }
        public DbSet<Tag>Tags { get; set; }
        public DbSet<User>Users { get; set; }
        public DbSet<Role>Roles { get; set; }
        public DbSet<AuthToken>AuthTokens { get; set; }

    }
}