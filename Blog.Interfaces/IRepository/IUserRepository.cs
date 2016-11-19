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
    }
}