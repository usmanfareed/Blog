using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class AuthToken
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public User UserId { get; set; }
    }
}
