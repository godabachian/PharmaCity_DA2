using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.Test
{
    [TestClass]
    public class ErrorDTOTest
    {
        private const bool _IsSuccess = true;
        private const string _ErrorMessage = "Usuario ya existe";
        private const string _Content = "Usuario ya existe";
        private const int _Code = 500;

        [TestMethod]
        public void SetIsSuccessTest()
        {
            ErrorDTO errorDto = new ErrorDTO();
            errorDto.IsSuccess = _IsSuccess;
            Assert.AreEqual(errorDto.IsSuccess, _IsSuccess);
        }

        [TestMethod]
        public void SetErrorMessageTest()
        {
            ErrorDTO errorDto = new ErrorDTO();
            errorDto.ErrorMessage = _ErrorMessage;
            Assert.AreEqual(errorDto.ErrorMessage, _ErrorMessage);
        }

        [TestMethod]
        public void SetContentTest()
        {
            ErrorDTO errorDto = new ErrorDTO();
            errorDto.Content = _Content;
            Assert.AreEqual(errorDto.Content, _Content);
        }

        [TestMethod]
        public void SetCodeTest()
        {
            ErrorDTO errorDto = new ErrorDTO();
            errorDto.Code = _Code;
            Assert.AreEqual(errorDto.Code, _Code);
        }
    }
}
