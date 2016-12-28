using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.ViewModels
{
    public class ResetPassViewModel
    {
        public string Token { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required,Compare("Password", ErrorMessage = "The password and confirmation password do not match."),
            DataType(DataType.Password),DisplayName("Confirm Password")]

        public string ConfirmPassword { get; set; }
    }
}