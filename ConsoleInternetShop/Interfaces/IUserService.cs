using ConsoleEShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEShop.Interfaces
{
    public interface IUserService
    {
        User Register(User user, string password);
        User Login(string username, string password);
        bool UserExists(string username);
        void UpdateUser(IUser user, string newEmail);
        IReadOnlyList<User> GetUsers();
    }
}
