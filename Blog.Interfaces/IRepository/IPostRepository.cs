using System.Collections.Generic;
using Blog.Models;

namespace Blog.Interfaces.IRepository
{
    public interface IPostRepository
    {
        bool IsExist(string title);
        Post GetBySlug(string slug);
        void SavePost(Post post, List<Tag> list);
    }
}