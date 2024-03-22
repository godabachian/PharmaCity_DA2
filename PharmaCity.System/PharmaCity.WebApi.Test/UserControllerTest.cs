using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Controllers;
using System.Collections.Generic;

namespace PharmaCity.WebApi.Test
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IUserService> _mock;
        private UserController _api;
        private User _admin;
        private Invitation _invitation;
        private UserDTO _adminDTO;
        private IEnumerable<User> users;

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IUserService>(MockBehavior.Strict);
            _api = new UserController(_mock.Object);

            _admin = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Pharmacy = new Pharmacy()
            };

            _adminDTO = new UserDTO
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio"
            };

            _invitation = new Invitation
            {
                UserName = "PharmaCityUY",
                Code = "ABCD1234"
            };

            users = new List<User>() { _admin };
        }

        [TestMethod]
        public void PostUserTest()
        {
            _mock.Setup(x => x.InsertUser(_admin, _invitation.Code)).Returns(_adminDTO);
            var result = _api.PostUser(_admin, _invitation.Code);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as UserDTO;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }
    }
}
