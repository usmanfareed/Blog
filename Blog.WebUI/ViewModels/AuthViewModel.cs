using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.WebUI.Areas.AdminPanel.ViewModels;

namespace Blog.WebUI.ViewModels
{
    public class AuthViewModel
    {


        public LoginModel Login { get; set; }
        public CreateUserVIewModel Register { get; set; }
        

       
    }

    public class LoginModel
    {
        [Required,MaxLength(50),DisplayName("USER NAME")]
        public string UserName { get; set; }


        [Required,DataType(DataType.Password), DisplayName("PASSWORD")]
        public string Password { get; set; }
    }


}