using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Blog.Models;

namespace Blog.WebUI.Areas.AdminPanel.ViewModels
{
    public class CreateUserVIewModel
    {
        public int Id { get; set; }
        [Required, DisplayName("Full Name"), MaxLength(50),RegularExpression(@"^[a-zA-Z+\s]+$", ErrorMessage = "Alphabets Only")]
        public string FullName { get; set; }
        [Required, DisplayName("User Name"), MaxLength(50)]
        public string UserName { get; set; }


        [Required, DisplayName("Email Address"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required,DisplayName("Password"), DataType(DataType.Password)]
        public string Password{ get; set; }

        [Required,DisplayName("Confirm Password"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword{ get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public class RoleViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }


        }
    }
}