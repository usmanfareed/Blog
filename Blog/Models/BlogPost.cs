using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public bool EnableComments { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DeletedData { get; set; }
        public bool Active { get; set; }
        public int Views { get; set; }
        public string SourceCode { get; set; }
        public int UserId { get; set; }

        public List<BlogComment> Comments { get; set; }
        public List<BlogTag> Tags { get; set; }
    }
}