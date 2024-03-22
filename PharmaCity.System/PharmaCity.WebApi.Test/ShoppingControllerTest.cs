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
    public class ShoppingControllerTest
    {
        private Mock<IShoppingService> _mockShopping;
        private ShoppingController _shoppingApi;
        private string _token;
        private string _code;
        private Petition _petition;
        private PetitionBuy _petitionBUY;
        private PetitionDTO _petitionDTO;
        private IEnumerable<PetitionDTO> _petitions;
        private PurchaseDTO _purchaseDTO;
        private IEnumerable<PurchaseDTO> _shoppingDTO;

        [TestInitialize]
        public void Setup()
        {
            _mockShopping = new Mock<IShoppingService>(MockBehavior.Strict);
            _shoppingApi = new ShoppingController(_mockShopping.Object);

            _token = "ABC123";
            _code = "CDF1234";

            _petition = new Petition
            {
                Id = 1,
                MedicineCode = "Paracetamol5",
                Quantity = 5,
                Pharmacy = new Pharmacy(),
                State = State.Pending
            };

            _petitionBUY = new PetitionBuy
            {
                Id = 1,
                MedicineCode = "Paracetamol5",
                Quantity = 5,
                Pharmacy = new Pharmacy(),
                State = State.Pending
            };

            _petitionDTO = new PetitionDTO
            {
                Id = _petition.Id,
                MedicineCode = _petition.MedicineCode,
                Quantity = _petition.Quantity,
                PharmacyName = _petition.Pharmacy.Name,
                State = _petition.State.ToString()
            };

            _purchaseDTO = new PurchaseDTO()
            {
                Id = 0,
                PurchaseCode = "RandomCode",
                PharmacyName = "PharmaCity",
                Shopping = new List<PetitionBuy> { _petitionBUY },
                State = State.Pending.ToString()
            };

            _petitions = new List<PetitionDTO> { _petitionDTO };

            _shoppingDTO = new List<PurchaseDTO> { _purchaseDTO };
        }

        [TestMethod]
        public void GetPetitionsBuyTest()
        {
            _mockShopping.Setup(x => x.GetPetitions(_token)).Returns(_petitions);

            var result = _shoppingApi.GetPetitions(_token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IList<Petition>;

            _mockShopping.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetPurchaseStateTest()
        {
            _mockShopping.Setup(x => x.GetPurchaseState(_code)).Returns(It.IsAny<string>());

            var result = _shoppingApi.GetPurchaseState(_code);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as string;

            _mockShopping.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetPharmacyTest()
        {
            _mockShopping.Setup(x => x.GetShopping()).Returns(_shoppingDTO);
            var result = _shoppingApi.GetShoppings();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<PurchaseDTO>;
            
            _mockShopping.VerifyAll();
            Assert.AreEqual(200, statusCode);
         }

        [TestMethod]
        public void PatchAcceptRequestTest()
        {
            _mockShopping.Setup(x => x.AcceptRequest(It.IsAny<int>(),It.IsAny<string>()));

            var result = _shoppingApi.PatchAcceptRequest(It.IsAny<int>(),It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as string;

            _mockShopping.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }
        [TestMethod]
        public void PatchDeclineRequestTest()
        {
            _mockShopping.Setup(x => x.DeclineRequest(It.IsAny<int>(), It.IsAny<string>()));

            var result = _shoppingApi.PatchDeclineRequest(It.IsAny<int>(), It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as string;
            
            _mockShopping.VerifyAll();
            Assert.AreEqual(200, statusCode);
         }
        
    }
}
