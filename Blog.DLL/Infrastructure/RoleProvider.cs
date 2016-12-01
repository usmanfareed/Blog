using System;
using System.Linq;
using Blog.DAL.Data;
using Blog.DAL.Repositories;
using Blog.Interfaces.IRepository;

namespace Blog.DLL.Infrastructure
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private readonly IUserRepository _userRepository = new UserRepository(new BlogDbContext());

        //public RoleProvider(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}


        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }


        public override string[] GetRolesForUser(string username)
        {
            return _userRepository.UserRoles(username); ;  
        }


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
