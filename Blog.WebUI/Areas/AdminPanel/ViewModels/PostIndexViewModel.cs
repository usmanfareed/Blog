using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.WebUI.Areas.AdminPanel.ViewModels
{
    public class PostIndexViewModel:Post
    {
        public string[] SelectedTags { get; set; }
    }
}