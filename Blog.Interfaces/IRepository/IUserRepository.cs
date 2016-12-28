using System.Collections.Generic;
using Blog.Models;

namespace Blog.Interfaces.IRepository
{
    public interface IUserRepository
    {

        bool IsExist(string user);
        ICollection<User> GetUsers();
        User GetUserById(int id);
        void UpdateUser(User user);

        User GetUser(string username,string email);

        string[] UserRoles(string username);
        string SaveToken(AuthToken token);
        int GetIdByToken(string token);
        string ResetPass(string pass, string token);
    }
}