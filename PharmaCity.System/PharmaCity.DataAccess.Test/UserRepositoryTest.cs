using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmaCity.DataAccess.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private User _admin;
        private PharmaCityDbContext _context;
        private IUserRepository _userRepository;

        [TestInitialize]
        public void Setup()
        {
            _admin = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                RegisterDate = DateTime.Now,
                Token = "ABC1234",
                Role = RoleType.Administrator
            };

            _context = CreateContext();
            _userRepository = new UserRepository(_context);
        }

        [TestMethod]
        public void InsertUserTest()
        {
            _userRepository.InsertUser(_admin);

            User userInDataBase = _context.Users.Where<User>(user => user.Id == _admin.Id).AsNoTracking().FirstOrDefault();

            Assert.AreEqual(_admin.UserName, userInDataBase.UserName);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            IEnumerable<User> usersRepository = _userRepository.GetUsers();

            Assert.AreEqual(usersRepository.Count(), 1);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            _userRepository.DeleteUser(_admin);

            User userInDataBase = _context.Users.Where<User>(user => user.Id == _admin.Id).AsNoTracking().FirstOrDefault();

            Assert.IsNull(userInDataBase);
        }

        [TestMethod]
        public void GetUserByEmailTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            User user = _userRepository.GetUserByEmail(_admin.Email);

            Assert.AreEqual(user.UserName, _admin.UserName);
        }

        [TestMethod]
        public void GetUserByTokenTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            User user = _userRepository.GetUserByToken(_admin.Token);

            Assert.AreEqual(user.UserName, _admin.UserName);
        }

        [TestMethod]
        public void ExistsUserTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            Assert.IsTrue(_userRepository.Exists(_admin.Email));
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            _userRepository.Update(_admin);

            Assert.IsTrue(_userRepository.Exists(_admin.Email));
        }

        [TestMethod]
        public void ValidLoginTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            _userRepository.ValidLogin(_admin.Email,_admin.Password);

            Assert.IsTrue(_userRepository.Exists(_admin.Email));
        }

        [TestMethod]
        public void IsAllowedTest()
        {
            _context.Users.Add(_admin);
            _context.SaveChanges();

            Assert.IsTrue(_userRepository.IsAllowed(_admin.Role,_admin.Token));
        }

        private PharmaCityDbContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<PharmaCityDbContext>()
                .UseInMemoryDatabase("UsersDb")
                .Options;

            var context = new PharmaCityDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
