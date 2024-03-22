using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
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
    public class ShoppingServiceTest
    {
        private Mock<IPetitionRepository> _mockPetition;
        private IShoppingService _shoppingService;

        private Mock<IUserRepository> _mockUser;
        private Mock<IShoppingRepository> _mockShopping;

        private Mock<IPharmacyRepository> _mockPharmacy;
        private Mock<IMedicineRepository> _mockMedicine;
        private Mock<IMedicineService> _mockMedicineService;
        private PetitionBuy _petitionBuy;
        private IEnumerable<Petition> _petitions;
        private User _user;
        private Purchase _purchase;

        private IEnumerable<Purchase> _shopping;


        private Pharmacy _pharmacy;
        private Medicine _medicine;

        [TestInitialize]
        public void Setup()
        {
            _mockPetition = new Mock<IPetitionRepository>(MockBehavior.Strict);
            _mockUser = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockShopping = new Mock<IShoppingRepository>(MockBehavior.Strict);
            _mockPharmacy = new Mock<IPharmacyRepository>(MockBehavior.Strict);
            _mockMedicine = new Mock<IMedicineRepository>(MockBehavior.Strict);
            _mockMedicineService = new Mock<IMedicineService>(MockBehavior.Strict);
            _shoppingService = new ShoppingService(_mockPetition.Object, _mockUser.Object, _mockShopping.Object, _mockPharmacy.Object, _mockMedicine.Object, _mockMedicineService.Object);

            _medicine = new Medicine
            {
                Code = "code",
                Name = "name",
                PharmacyName = "pharmacy",
                Stock = 10000,
            };
            _pharmacy = new Pharmacy
            {
                Name = "pharmacy",
                Direction = "def dir",
                Id = 100,
                Medicines = new List<Medicine>(){ _medicine }
            };
            
            _petitionBuy = new PetitionBuy
            {
                Id = 1,
                MedicineCode = "ABC123",
                Pharmacy = _pharmacy, //new Pharmacy(),
                State = State.Pending,
                Quantity = 10
            };

            _user = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Pharmacy = _pharmacy,//new Pharmacy(),
                Role = RoleType.Administrator,
                Token = "ABCD1234"
            };

            _purchase = new Purchase()
            {
                Id = 1,
                Code = "ABCD1234",
                Pharmacy = _pharmacy,//new Pharmacy(),
                Shopping = new List<PetitionBuy> { _petitionBuy },
                State = State.Pending
            };


            _petitions = new List<PetitionBuy>() { _petitionBuy };

            _shopping = new List<Purchase>() { _purchase };
        }

        [TestMethod]
        public void GetPetitionsBuyTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPetition.Setup(x => x.GetPetitionsBuy(_user.Pharmacy.Name)).Returns(_petitions);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _shoppingService.GetPetitions(_user.Token);
            _mockPetition.VerifyAll();
        }

        [TestMethod]
        public void GetPurchaseStatusTest()
        {
            _mockShopping.Setup(x => x.ExistsCode(_purchase.Code)).Returns(true);
            _mockShopping.Setup(x => x.GetShoppingStateByCode(_purchase.Code)).Returns(_purchase.State.ToString());
            
            _shoppingService.GetPurchaseState(_user.Token);
            _mockShopping.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No existe una compra con el codigo ingresado.")]
        public void GetNotExistsPurchaseStatusTest()
        {
            _mockShopping.Setup(x => x.ExistsCode(_purchase.Code)).Returns(false);

            _shoppingService.GetPurchaseState(_user.Token);
            _mockShopping.VerifyAll();
        }

        [TestMethod]

        public void GetShoppingTest()
        {
            _mockShopping.Setup(x => x.GetShopping()).Returns(_shopping);

            _shoppingService.GetShopping();
        }

        [TestMethod]
        public void AcceptRequestTest()
        {
            _mockShopping.Setup(x => x.Exists(_purchase.Id)).Returns(true);
            _mockShopping.Setup(x => x.GetPurchaseById(_purchase.Id)).Returns(_purchase);
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _mockMedicineService.Setup(x => x.CheckAllMedicinesStock(_purchase));
            _mockShopping.Setup(x => x.Update(_purchase));
            _mockPetition.Setup(x => x.Update(_petitionBuy));
            _mockMedicine.Setup(x => x.GetMedicineByCode(It.IsAny<string>())).Returns(_medicine);
            _mockMedicine.Setup(x => x.Update(_medicine));
            _shoppingService.AcceptRequest(_purchase.Id,_user.Token);
            _mockShopping.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AcceptInvalidRequest()
        {
            _mockShopping.Setup(x => x.Exists(_purchase.Id)).Returns(false);
            _shoppingService.AcceptRequest(_purchase.Id,_user.Token);
            _mockShopping.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AcceptInactiveRequest()
        {
            _mockShopping.Setup(x => x.Exists(_purchase.Id)).Returns(true);
            _mockShopping.Setup(x => x.GetPurchaseById(_purchase.Id)).Returns(_purchase);
            _purchase.State = State.Active;
            _shoppingService.AcceptRequest(_purchase.Id, _user.Token);
            _mockShopping.VerifyAll();
        }

        [TestMethod]
        public void DeclineRequestTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockShopping.Setup(x =>x.GetPurchaseById(It.IsAny<int>())).Returns(_purchase);
            _mockPetition.Setup(x => x.Update(_petitionBuy));
            _mockShopping.Setup(x => x.Update(_purchase));
            _shoppingService.DeclineRequest(_purchase.Id, _user.Token);
            _mockShopping.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeclineNullPurchaseTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockShopping.Setup(x => x.GetPurchaseById(It.IsAny<int>())).Returns(_purchase);
            _purchase.State = State.Inactive;
            _shoppingService.DeclineRequest(_purchase.Id, _user.Token);
            _mockShopping.VerifyAll();
        }

        [TestMethod]
        public void GetPurchaseStateTest()
        {
            _mockShopping.Setup(x => x.ExistsCode(It.IsAny<string>())).Returns(true);
            _mockShopping.Setup(x => x.GetShoppingStateByCode(It.IsAny<string>())).Returns(_purchase.State.ToString);
            _shoppingService.GetPurchaseState(_purchase.Code);
            _mockShopping.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNullPurchaseStateTest()
        {
            _mockShopping.Setup(x => x.ExistsCode(It.IsAny<string>())).Returns(false);
            _shoppingService.GetPurchaseState(_purchase.Code);

            _mockShopping.VerifyAll();
        }
    }
}
