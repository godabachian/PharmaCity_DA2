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
    public class StockRequestControllerTest
    {
        private Mock<IStockRequestService> _mock;
        private StockRequestController _api;
        private StockRequest _stockRequest;
        private StockRequestDTO _stockRequestDto;
        private IEnumerable<StockRequestDTO> _stockRequestsDTOs;

        private int _stockRequestID = 1;
        private string _token = "ABC1234";

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IStockRequestService>(MockBehavior.Strict);
            _api = new StockRequestController(_mock.Object);

            _stockRequest = new StockRequest
            {
                Employee = new User(),
                Petitions = new List<PetitionStock>(),
                Pharmacy = new Pharmacy(),
                State = State.Active
            };

            _stockRequestDto = new StockRequestDTO
            {
                EmployeeUserName = _stockRequest.Employee.UserName,
                Petitions = _stockRequest.Petitions
            };

            _stockRequestsDTOs = new List<StockRequestDTO>() { _stockRequestDto };
        }

        [TestMethod]
        public void PostStockRequestTest()
        {
            _mock.Setup(x => x.InsertRequest(_stockRequest, _token)).Returns(_stockRequestDto);

            var result = _api.PostRequest(_stockRequest, _token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as StockRequest;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetStockRequestsTest()
        {
            _mock.Setup(x => x.GetRequests(_token)).Returns(_stockRequestsDTOs);

            var result = _api.GetRequests(_token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IList<StockRequest>;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void PutAcceptStockRequestsTest()
        {
            _mock.Setup(x => x.AcceptRequest(_stockRequestID, _token));

            var result = _api.PatchAcceptRequest(_stockRequestID, _token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IList<StockRequest>;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void PutDeclineStockRequestsTest()
        {
            _mock.Setup(x => x.DeclineRequest(_stockRequestID, _token));

            var result = _api.PatchDeclineRequest(_stockRequestID, _token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IList<StockRequest>;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }
    }
}
