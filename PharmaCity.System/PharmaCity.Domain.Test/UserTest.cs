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
    public class UserTest
    {
        private const string _invalidEmailTest = "pharmacity/gmail.com";
        private const string _invalidPassword = "invalidPassword";
        private const string _invalidUserName = "Pharma City";
        private const string _invalidLengthPassword = "Pass!";
        private const string _empty = "";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El email no puede ser nulo")]
        public void SetNullEmailTest()
        {
            User user = new User()
            {
                Email = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El email debe cumplir la cantida de caracteres")]
        public void SetInvalidLengthEmailTest()
        {
            User user = new User()
            {
                Email = _empty
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Debe cumplir el formato de email")]
        public void SetInvalidEmailTest()
        {
            User user = new User()
            {
                Email = _invalidEmailTest
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "La contraseña no debe ser vacía")]
        public void SetNullPasswordTest()
        {
            User user = new User()
            {
                Password = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La contraseña debe tener como mínimo 8 caracteres")]
        public void SetInvalidLengthPasswordTest()
        {
            User user = new User()
            {
                Password = _invalidLengthPassword
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La contraseña debe tener al menos un caracter especial")]
        public void SetInvalidPasswordTest()
        {
            User user = new User()
            {
                Password = _invalidPassword
            };
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
        [ExpectedException(typeof(InvalidOperationException), "El nombre de usuario debe contar con cierta cantidad de caracteres")]
        public void SetInvalidLengthUserNameTest()
        {
            User user = new User()
            {
                UserName = _empty
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El nombre de usuario no debe ser nulo")]
        public void SetNullUserNameTest()
        {
            User user = new User()
            {
                UserName = null
            };
        }
    }
}
