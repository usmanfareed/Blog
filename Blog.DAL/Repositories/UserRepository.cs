using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Data;
using Blog.Interfaces.IRepository;
using Blog.Models;

namespace Blog.DAL.Repositories
{
   public class UserRepository:RepositoryBase<User>, IUserRepository
   {
        public UserRepository(BlogDbContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

        }

        BlogDbContext db = new BlogDbContext();

        public bool IsExist(string user)
        {
            return db.Users.Any(x => x.UserName == user);
        }

       public ICollection<User> GetUsers()
       {
           return db.Users.Include("Roles").ToList();
       }
       public User GetUserById(int id)
       {
           return db.Users.Include("Roles").Single(x=>x.Id == id);
       }

       public void UpdateUser(User user)
       {
          var roles = db.Roles.ToList();
          var getUser= GetUserById(user.Id);

           getUser.Email = user.Email;
           getUser.FullName = user.FullName;
           getUser.UserName = user.UserName;
           getUser.PasswordHash = user.PasswordHash;

           getUser.Roles.Clear();

           foreach (var role in roles)
           {
               if (user.Roles.Any(x=>x.Id == role.Id))
               {
                    getUser.Roles.Add(role);
                }

              
           }
         
           db.SaveChanges();

       }



       public User LoginUser(string username)
       {
           return db.Users.SingleOrDefault(x => x.UserName == username);
       }


       public string[] UserRoles(string username)
       {

            //string[] roles = db.Users
            //    .Where(x => x.Id == x.Roles.Select(y => y.Id).SingleOrDefault())
            //    .Select(x => x.Roles.Select(y => y.Name)).ToArray();

            return db.Roles
               .Where(x => x.Users.Any(y => y.UserName == username))
               .Select(x => x.Name).ToArray();
            ;

        }
     }
}
