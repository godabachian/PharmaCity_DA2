using Microsoft.EntityFrameworkCore;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private PharmaCityDbContext _dbContext;

        public UserRepository(PharmaCityDbContext context)
        {
            this._dbContext = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public bool Exists(string email)
        {
            return _dbContext.Users.Any(user => user.Email == email);
        }

        public bool ValidLogin(string email, string password)
        {
            try
            {
                return _dbContext.Users.Any(user => user.Email == email && user.Password == password);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error inesperado con la base de datos, intente mas tarde...");
            }
        }

        public void Update(User user)
        {
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }

        public bool IsAllowed(RoleType role, string token)
        {
            return _dbContext.Users.Any(user => user.Role == role && user.Token == token);
        }

        public User GetUserByToken(string token)
        {
            return _dbContext.Users.Include(user => user.Pharmacy).ThenInclude(pharmacy => pharmacy.Medicines).FirstOrDefault(user => user.Token == token);
        }
    }
}
