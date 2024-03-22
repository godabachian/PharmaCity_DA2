using PharmaCity.Domain;
using System;
using System.Collections.Generic;

namespace PharmaCity.IDataAccess
{
    public interface IUserRepository
    {
        void InsertUser(User user);
        IEnumerable<User> GetUsers();
        void DeleteUser(User user);
        User GetUserByEmail(string email);
        bool Exists(string email);
        bool ValidLogin(string email, string password);
        void Update(User user);
        bool IsAllowed(RoleType role, string token);
        User GetUserByToken(string token);
    }
}
