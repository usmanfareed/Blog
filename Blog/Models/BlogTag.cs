using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogTag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DeletedData { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }

        public List<BlogPost> Posts { get; set; }

    }
}