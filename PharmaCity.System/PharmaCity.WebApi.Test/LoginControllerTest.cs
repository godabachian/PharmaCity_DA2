using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.WebApi.Test
{
    [TestClass]
    public class LoginControllerTest
    {
        private Mock<ISessionService> _mock;
        private LoginController _api;
        private User _admin;
        private LoginDTO _loginDTO;
        private Login _login;

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<ISessionService>(MockBehavior.Strict);
            _api = new LoginController(_mock.Object);

            _admin = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Token = "ABCD1234YZ",
                Role = RoleType.Administrator
            };

            _login = new Login
            {
                Email = _admin.Email,
                Password = _admin.Password
            };

            _loginDTO = new LoginDTO
            {
                Token = _admin.Token,
                User = _admin.UserName,
                Role = _admin.Role.ToString()
            };
        }

        [TestMethod]
        public void LoginUserTest()
        {
            _mock.Setup(x => x.Login(_login)).Returns(_loginDTO);

            var result = _api.LoginUser(_login);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as User;

            Assert.AreEqual(200, statusCode);
        }
    }
}
