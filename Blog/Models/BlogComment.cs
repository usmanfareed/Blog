using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Blog.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        // User id of comment 
        public int PostId { get; set; }

        public int ReplyToId { get; set; }

    }
}