using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;

namespace PharmaCity.BusinessLogic.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserRepository> _mockUser;
        private Mock<IInvitationRepository> _mockInvitation;
        private IUserService userService;
        private User _admin;
        private UserDTO _adminDTO;
        private Invitation _invitation;
        private IEnumerable<User> _users;
        private IEnumerable<UserDTO> _usersDtos;

        [TestInitialize]
        public void Setup()
        {
            _mockUser = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockInvitation = new Mock<IInvitationRepository>(MockBehavior.Strict);
            userService = new UserService(_mockUser.Object, _mockInvitation.Object);

            _admin = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Pharmacy = new Pharmacy(),
                Role = RoleType.Administrator
            };

            _adminDTO = new UserDTO
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Role = RoleType.Administrator
            };

            _invitation = new Invitation
            {
                UserName = "PharmaCityUY",
                Code = "ABCD1234",
                Role = RoleType.Administrator,
                Pharmacy = new Pharmacy()
            };

            _users = new List<User>() { _admin };
            _usersDtos = new List<UserDTO>() { _adminDTO };
        }

        [TestMethod]
        public void InsertUserTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(false);
            _mockInvitation.Setup(x => x.ExistsByCodeAndUserName(_invitation.Code, _invitation.UserName)).Returns(true);
            _mockInvitation.Setup(x => x.GetInvitationByCode(_invitation.Code)).Returns(_invitation);
            _mockUser.Setup(x => x.InsertUser(_admin));
            _mockInvitation.Setup(x => x.Update(_invitation));

            userService.InsertUser(_admin, _invitation.Code);
            _mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El usuario ya existe")]
        public void InsertExistUserTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(true);
            _mockInvitation.Setup(x => x.ExistsByCodeAndUserName(_invitation.Code, _admin.UserName)).Returns(true);
            _mockUser.Setup(x => x.InsertUser(_admin));
            userService.InsertUser(_admin, _invitation.Code);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "La invitación al usuario no existe")]
        public void InsertInvalidInvitationUserTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(false);
            _mockInvitation.Setup(x => x.ExistsByCodeAndUserName(_invitation.Code, _admin.UserName)).Returns(false);
            _mockUser.Setup(x => x.InsertUser(_admin));
            _mockUser.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            userService.InsertUser(_admin, _invitation.Code);
            _mockUser.VerifyAll();
        }

        [TestMethod]
        public void GetUsersTest()
        {
            _mockUser.Setup(x => x.GetUsers()).Returns(_users);
            userService.GetUsers();
            _mockUser.VerifyAll();
        }

        [TestMethod]
        public void DeleteExistUserTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(true);
            _mockUser.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            _mockUser.Setup(x => x.DeleteUser(_admin));
            userService.DeleteUser(_admin.Email);
            _mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No existe un usuario con ese email")]
        public void DeleteNotExistUserTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(false);
            _mockUser.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            _mockUser.Setup(x => x.DeleteUser(_admin));
            userService.DeleteUser(_admin.Email);
            _mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No existe un usuario con ese email")]
        public void NotExistUserByEmailTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(false);
            _mockUser.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            _mockUser.Setup(x => x.DeleteUser(_admin));
            userService.GetUserByEmail(_admin.Email);
        }

        [TestMethod]
        public void ExistUserByEmailTest()
        {
            _mockUser.Setup(x => x.Exists(_admin.Email)).Returns(true);
            _mockUser.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            _mockUser.Setup(x => x.DeleteUser(_admin));
            userService.GetUserByEmail(_admin.Email);
        }

        

    }
}
