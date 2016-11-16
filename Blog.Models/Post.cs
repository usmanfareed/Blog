﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool EnableComments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public int Views { get; set; }
        public int UserId { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }



        public Post()
        {
            CreatedAt = DateTime.Now;
            Active = true;
            EnableComments = true;
        }
    }

   
}