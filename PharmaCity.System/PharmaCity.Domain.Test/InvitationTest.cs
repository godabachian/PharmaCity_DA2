using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.Test
{
    [TestClass]
    public class InvitationTest
    {
        private const int _Id = 1;
        private const string _UserNameTest = "PharmaCity";
        private const RoleType _RoleTest = RoleType.Employee;
        private const string _CodeTest = "ABC123";
        private Pharmacy _PharmacyTest = new Pharmacy();
        private string _invalidUserName = "Pharma City";

        [TestMethod]
        public void CreateInvitationTest()
        {
            Invitation invitation = new Invitation();
            Assert.IsNotNull(invitation);
        }

        [TestMethod]
        public void SetIdTest()
        {
            Invitation invitation = new Invitation();
            invitation.Id = _Id;
            Assert.AreEqual(invitation.Id, _Id);
        }

        [TestMethod]
        public void SetUserNameTest()
        {
            Invitation invitation = new Invitation
            {
                UserName = _UserNameTest
            };
            Assert.AreEqual(invitation.UserName, _UserNameTest);
        }

        [TestMethod]
        public void SetRoleTest()
        {
            Invitation invitation = new Invitation
            {
                Role = _RoleTest
            };
            Assert.AreEqual(invitation.Role, _RoleTest);
        }

        [TestMethod]
        public void SetCodeTest()
        {
            Invitation invitation = new Invitation
            {
                Code = _CodeTest
            };
            Assert.AreEqual(invitation.Code, _CodeTest);
        }

        [TestMethod]
        public void SetPharmacyTest()
        {
            Invitation invitation = new Invitation
            {
                Pharmacy = _PharmacyTest
            };
            Assert.IsNotNull(invitation.Pharmacy);
        }

        [TestMethod]
        public void SetStateTest()
        {
            Invitation invitation = new Invitation
            {
                State = State.Active
            };
            Assert.IsNotNull(invitation.State);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El nombre de usuario no debe contener espacios")]
        public void SetInvalidUserNameTest()
        {
            User user = new User()
            {
                UserName = _invalidUserName
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El nombre de usuario no debe ser nulo")]
        public void SetNullUserNameTest()
        {
            User user = new User
            {
                UserName = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El nombre de usuario debe contar con cierta cantidad de caracteres")]
        public void SetInvalidLengthUserNameTest()
        {
            User user = new User()
            {
                UserName = ""
            };
        }
    }
}
