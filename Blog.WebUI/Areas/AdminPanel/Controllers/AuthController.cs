using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Blog.DAL.Data;
using Blog.DLL;
using Blog.Interfaces.IRepository;
using Blog.Models;
using Blog.WebUI.Areas.AdminPanel.ViewModels;
using Blog.WebUI.ViewModels;

namespace Blog.WebUI.Areas.AdminPanel.Controllers
{
    [RouteArea("AdminPanel")]
    [RoutePrefix("Users")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepositoryBase<Role> _role;
        private readonly IRepositoryBase<User> _userRepositoryBase;

        public AuthController(IUserRepository userRepository,IRepositoryBase<Role> role,IRepositoryBase<User> userRepositoryBase )
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
            return View("Index",user);
        }

        [Route("createuser")]
        [ValidateAntiForgeryToken]

        public ActionResult CreateUser(CreateUserVIewModel user)
        {
            ViewBag.IsNew = true;
            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }


            if (_userRepository.IsExist(user.UserName))
            {
                ModelState["UserName"].Errors.Add("User already exists ");
                return View("Index",user);


            }
            //var roles = user.Roles.Select(x => new Role()
            //{
            //    Id = x.Id,
            //    Name = x.Name

            //}).ToList();

            var newuser = new User
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = DateTime.Now,
                PasswordHash = HashPassword.SetPassword(user.Password),

                Roles = user.Roles.Where(x=>x.IsChecked).Select(x => new Role
                {

                    Id = x.Id,
                    Name = x.Name
                }).ToList()


            };

            //var data = db.Users
            //    .Include("Roles")
            //    .Single(x=>x.Id ==1);




            //var roles = user.Roles.Select(x => new Role()
            //{
            //    Id = x.Id,
            //    Name = x.Name

            //}).ToList();

            

            //var roles = db.Roles.Select(x=>x).ToList();

            //data.Roles.Clear();
            ////_userRepositoryBase.Update(newuser);
            //foreach (var role in roles)
            //{
            //    newuser.Roles.Add(role);
            //}
            //db.Users.Attach(newuser);
            //db.Users.Add(newuser);
            //db.SaveChanges();

            _userRepositoryBase.AddUpdate(newuser);
            _userRepositoryBase.Commit();

            return RedirectToAction("Users");
        }

        //Displays all the register users
        [Route("")]
        public ActionResult Users()
        {
            var users = _userRepository.GetUsers();
            return View("Users",users);
        }



         [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _userRepositoryBase.Delete(id);
            _userRepositoryBase.Commit();

            return RedirectToAction("Users");
        }


         [Route("update")]
        public ActionResult Update(CreateUserVIewModel user)
        {
            ViewBag.IsNew = false;

            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }





            var newuser = new User
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = HashPassword.SetPassword(user.Password),
                Roles = user.Roles.Where(x => x.IsChecked).Select(x => new Role
                {

                    Id = x.Id,
                    Name = x.Name
                }).ToList()


            };



            _userRepository.UpdateUser(newuser);


            //newuser.Roles.Clear();

            //var roles = user.Roles.Select(x => new Role()
            //{
            //    Id = x.Id,
            //    Name = x.Name

            //}).ToList();


            //foreach (var role in roles)
            //{
            //    newuser.Roles.Add(role);
            //}

            
            
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
                 Roles = roles.Select(x=> new CreateUserVIewModel.RoleViewModel()
                 {
                     Id = x.Id ,
                     Name = x.Name,
                     IsChecked = getUser.Roles.Any(z => z.Id == x.Id)
                 }).ToList()
             };

             return View("Index", user);

         }


        [Route("login")]
        public ActionResult Login(AuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Account/Index.cshtml",model);
            }


            var user =   _userRepository.LoginUser(model.Login.UserName);

            if (HashPassword.CheckPassword(model.Login.Password , user.PasswordHash))
            {
               return  RedirectToAction("Index");
            }

            return RedirectToAction("Index","Account");
        }



    }
}