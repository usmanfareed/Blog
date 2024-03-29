﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Blog.DAL.Data;
using Blog.DLL;
using Blog.Interfaces.IRepository;
using Blog.Models;
using Blog.WebUI.Areas.AdminPanel.ViewModels;
using Blog.WebUI.ViewModels;
using Microsoft.AspNet.Identity;

namespace Blog.WebUI.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    [RouteArea("AdminPanel")]
    [RoutePrefix("Users")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepositoryBase<Role> _role;
        private readonly IRepositoryBase<User> _userRepositoryBase;

        public AuthController(IUserRepository userRepository, IRepositoryBase<Role> role,
            IRepositoryBase<User> userRepositoryBase)
        {
            _userRepository = userRepository;
            _role = role;
            _userRepositoryBase = userRepositoryBase;
        }

        // GET: AdminPanel/Auth
        [Route("create")]
        public ActionResult Index()
        {
            ViewBag.IsNew = true;
            var user = new CreateUserVIewModel();

            var role = _role.GetAll().ToList();

            user.Roles = role.Select(x => new CreateUserVIewModel.RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IsChecked = false
            }).ToList();

            var authViewMode = new AuthViewModel {Register = user};

            return View("Index", authViewMode);
        }

        [Route("createuser")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult CreateUser(AuthViewModel userRegister, bool IsUserRegister)
        {
            ViewBag.IsNew = true;
            if (!ModelState.IsValid)
            {
                if (IsUserRegister)
                {
                    //return RedirectToAction("Index", "Account",new {area = "",user});
                    return View("~/Views/Account/Index.cshtml", userRegister);
                }
                return View("Index", userRegister);
            }


            if (_userRepository.IsExist(userRegister.Register.UserName))
            {
                if (IsUserRegister)
                {
                    ModelState["Register.UserName"].Errors.Add("User already exists ");
                    return View("~/Views/Account/Index.cshtml", userRegister);
                }

                ModelState["UserName"].Errors.Add("User already exists ");

                return View("Index", userRegister);
            }
            //var roles = user.Roles.Select(x => new Role()
            //{
            //    Id = x.Id,
            //    Name = x.Name

            //}).ToList();
            var newuser = new User
            {
                FullName = userRegister.Register.FullName,
                UserName = userRegister.Register.UserName,
                Email = userRegister.Register.Email,
                CreatedAt = DateTime.Now,
                PasswordHash = HashPassword.SetPassword(userRegister.Register.Password),
                Roles = userRegister.Register.Roles?.Where(x => x.IsChecked).Select(x => new Role
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToList() ?? new List<Role>
                        {
                            new Role
                            {
                                Id = 3,
                                Name = "User"
                            }
                        }
            };


            _userRepositoryBase.AddUpdate(newuser);
            _userRepositoryBase.Commit();

            return RedirectToAction("Users");
        }

        //Displays all the register users
        [Route("")]
        public ActionResult Users()
        {
            var users = _userRepository.GetUsers();
            return View("Users", users);
        }


        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _userRepositoryBase.Delete(id);
            _userRepositoryBase.Commit();

            return RedirectToAction("Users");
        }


        [Route("update")]
        public ActionResult Update(AuthViewModel user)
        {
            ViewBag.IsNew = false;

            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }


            var newuser = new User
            {
                Id = user.Register.Id,
                FullName = user.Register.FullName,
                UserName = user.Register.UserName,
                Email = user.Register.Email,
                PasswordHash = HashPassword.SetPassword(user.Register.Password),
                Roles = user.Register.Roles.Where(x => x.IsChecked).Select(x => new Role
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };


            _userRepository.UpdateUser(newuser);


            return RedirectToAction("Users");
        }


        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            ViewBag.IsNew = false;
            var getUser = _userRepository.GetUserById(id);
            var roles = _role.GetAll().ToList();

            var user = new CreateUserVIewModel()
            {
                Id = getUser.Id,
                UserName = getUser.UserName,
                Email = getUser.Email,
                FullName = getUser.FullName,
                Roles = roles.Select(x => new CreateUserVIewModel.RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsChecked = getUser.Roles.Any(z => z.Id == x.Id)
                }).ToList()
            };

            var authViewMode = new AuthViewModel {Register = user};

            return View("Index", authViewMode);
        }


        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public ActionResult Login(AuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Account/Index.cshtml", model);
            }

            bool rememberme = true;
            int time = 20;

            var user = _userRepository.GetUser(model.Login.UserName, null);
            if (user == null)
            {
                return Content("invalid user");
            }

            if (HashPassword.CheckPassword(model.Login.Password, user.PasswordHash))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, rememberme);
                HttpCookie cookie = FormsAuthentication.GetAuthCookie(user.UserName, rememberme);
                cookie.Expires = DateTime.Now.AddMinutes(time);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                HttpCookie userCookie = new HttpCookie("FullName")
                {
                    Expires = DateTime.Now.AddMinutes(time),
                    Value = user.FullName
                };

                System.Web.HttpContext.Current.Response.Cookies.Add(userCookie);


                if (string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.Session["CurrentScreenName"]?.ToString()))
                {
                    System.Web.HttpContext.Current.Session["CurrentScreenName"] = user.FullName;
                }


                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Account");
        }

        [Route("logout")]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            if (System.Web.HttpContext.Current.Request.Cookies["FullName"] != null)
            {
                var del = new HttpCookie("FullName") {Expires = DateTime.Now.AddDays(-1)};
                Response.Cookies.Add(del);
            }

            return RedirectToAction("Index", "Home", new {area = ""});
        }

        //This action emails token to reset password

        [Route("gettoken")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SendToken(string email)
        {
            if (_userRepository.GetUser(null, email) != null)

            {
                AuthToken token = new AuthToken();

                var user = _userRepository.GetUser(null, email);

                token.UserId = user;
                token.Token = Base.GetToken();

                _userRepository.SaveToken(token);

                var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri);

                urlBuilder.Path = "AdminPanel/Users/verifytoken";
                urlBuilder.Query = null;
                    

                string url = urlBuilder.ToString();


              
                var result = Base.EmailToken(email, token.Token, url);


                return Content(result);
            }


            return Content("User Not Found");
        }


        [Route("verifytoken/{*token}")]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult VerifyToken(string token)
        {
            if (Base.CheckTokenExpiry(token))
            {
                //var id = _userRepository.GetIdByToken(token);
                //var user = _userRepository.GetUserById(id);


                if (_userRepository.GetIdByToken(token) != 0)
                {
                    var model = new ResetPassViewModel {Token = token};
                    return View("~/Views/Account/resetpass.cshtml",model);

                }



            }
            else
            {
                return Content("Token invalid or Token expired");
            }

            return null;
        }


        [Route("resetpass")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPass(ResetPassViewModel model)
        {
           return Content(_userRepository.ResetPass(HashPassword.SetPassword(model.Password), model.Token)); 
        }
    }
}