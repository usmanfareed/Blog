using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]


        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        
        [Required]
        public string Email { get; set; }



        [Required]
        public string PasswordHash { get; set; }

        public DateTime? LoginAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<AuthToken> AuthTokens { get; set; }



        public User()
        {
            CreatedAt = DateTime.Now;
        }
    }

}
