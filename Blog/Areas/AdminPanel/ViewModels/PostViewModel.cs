using System.Collections.Generic;
using Blog.Models;

namespace Blog.Areas.AdminPanel.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<ImagePostViewModel> Url { get; set; }
    }
}