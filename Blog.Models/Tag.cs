using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DeletedData { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
}