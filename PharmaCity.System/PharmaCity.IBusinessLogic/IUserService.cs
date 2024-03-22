using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using System;
using System.Collections.Generic;

namespace PharmaCity.IBusinessLogic
{
    public interface IUserService
    {
        UserDTO InsertUser(User user, string code);
        IEnumerable<UserDTO> GetUsers();
        void DeleteUser(string email);
        User GetUserByEmail(string email);
        
    }
}
