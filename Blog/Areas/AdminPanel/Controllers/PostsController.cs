﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.AdminPanel.Controllers
{
    public class PostsController : Controller
    {
        // GET: AdminPanel/Posts
        public ActionResult Index()
        {
            return View();
        }
    }
}