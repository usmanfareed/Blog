using System.Collections.Generic;
using Blog.Models;

namespace Blog.Interfaces.IRepository
{
    public interface IPostRepository
    {
        bool IsExist(string title);
        Post GetBySlug(string slug);
        void SavePost(Post post, List<Tag> list);
        void UpdatePost(Post post, List<Tag> list);

        List<Post> GetAllPosts();
        void UpdateViewCount(int id);
        List<Post> GetAllPostsByTag(string tag);
        Post GetById(int id);

    }
}