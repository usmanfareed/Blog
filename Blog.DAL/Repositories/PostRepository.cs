using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public Post GetBySlug(string slug)
        {
            return db.Posts.Include("Tags").FirstOrDefault(x => x.Slug == slug);
        }
        public override Post GetById(int id)
        {
            return db.Posts.Include("Tags").FirstOrDefault(x => x.Id == id);
        }

        public void SavePost(Post post, List<Tag> list)
        {
            db.Posts.Add(post);
            foreach (var tag in list)
            {
                db.Tags.Attach(tag);

            }
            db.SaveChanges();
        }

        public void UpdatePost(Post post, List<Tag> list)
        {
            foreach (var tag in list)
            {
                db.Tags.Attach(tag);

            }
            var find= db.Posts.Include("Tags").Single(x => x.Id == post.Id);
       
            find.Title = post.Title;
            find.Slug = post.Slug;
            find.Content = post.Content;
            find.Tags.Clear();
            find.Tags.AddRange(post.Tags);

           
            db.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
          return  db.Posts.Include("Tags").OrderByDescending(x=>x.CreatedAt).ToList();
        }
        
        public void UpdateViewCount(int id)
        {
            db.Database.ExecuteSqlCommand("UPDATE Posts SET Views = Views + 1 WHERE Id = @id", new SqlParameter("@id", id));
        }

        public List<Post> GetAllPostsByTag(string tag)
        {
            return db.Posts.Include("Tags").Where(x=>x.Tags.Any(y=>y.Title == tag)).OrderByDescending(x=>x.Views).ToList();
        }

        public List<Post> SearchPosts(string search)
        {
            return db.Posts.Include("Tags").Where(x => x.Title.Contains(search)).ToList();
        }
        public List<Tag> TopTags()
        {
            return db.Tags.OrderByDescending(x=>x.Posts.Sum(y=>y.Views)).ToList();
        }

        public Dictionary<int,IEnumerable<Post>> LoadArchives()
        {
            return db.Posts.GroupBy(x => x.CreatedAt.Year).OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.OrderByDescending(z=>z.CreatedAt).Select(z=>z));
        }
    }
}
