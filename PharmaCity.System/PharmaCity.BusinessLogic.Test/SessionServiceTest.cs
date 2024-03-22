using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic.Test
{
    [TestClass]
    public class SessionServiceTest
    {
        private Mock<IUserRepository> _mock;
        private ISessionService _sessionService;
        private Mock<IGuidService> _mockGuid;
        private User _admin;
        private IEnumerable<User> users;
        private Login _login;

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockGuid = new Mock<IGuidService>(MockBehavior.Strict);
            _sessionService = new SessionService(_mock.Object, _mockGuid.Object);

            _admin = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Token = "ABCD1234",
                Role = RoleType.Administrator
            };

            _login = new Login
            {
                Email = _admin.Email,
                Password = _admin.Password
            };


            users = new List<User>() { _admin };
        }

        [TestMethod]
        public void IsAllowedTest()
        {
            _mock.Setup(x => x.IsAllowed(_admin.Role, _admin.Token)).Returns(true);
            _sessionService.IsAllowed(_admin.Role, _admin.Token);

            _mock.VerifyAll();
        }

        [TestMethod]
        public void LoginTest()
        {
            _mock.Setup(x => x.ValidLogin(_admin.Email, "E+L5uktx7HCsPotRc+0RAwXEM58=")).Returns(true);
            _mock.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            _mockGuid.Setup(x => x.NewGuid()).Returns("ABC123");
            _mock.Setup(x => x.Update(_admin));

            _sessionService.Login(_login);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El correo o la contraseña son incorrectos")]
        public void LoginInvalidTest()
        {
            _mock.Setup(x => x.ValidLogin(_admin.Email, "E+L5uktx7HCsPotRc+0RAwXEM58=")).Returns(false);
            _mock.Setup(x => x.GetUserByEmail(_admin.Email)).Returns(_admin);
            _mockGuid.Setup(x => x.NewGuid()).Returns("ABC123");
            _mock.Setup(x => x.Update(_admin));

            _sessionService.Login(_login);
        }
    }
}
