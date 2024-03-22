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
    public class StockRequestServiceTest
    {
        private IStockRequestService _stockRequestService;
        private Mock<IStockRequestRepository> _mockStockRequest;
        private Mock<IUserRepository> _mockUser;
        private Mock<IPharmacyRepository> _mockPharmacy;
        private Mock<IMedicineRepository> _mockMedicine;


        private StockRequest _stockRequest;
        private StockRequest _stockRequestInactive;
        private StockRequest _stockRequestInvalid;
        private IEnumerable<StockRequest> _stockRequests;
        private User _user;
        private Pharmacy _pharmacy;
        private PetitionStock _petition;
        private Medicine _medicine;
        private IEnumerable<Petition> _petitions;
        private IEnumerable<Medicine> _medicines;

        [TestInitialize]
        public void Setup()
        {
            _mockStockRequest = new Mock<IStockRequestRepository>(MockBehavior.Strict);
            _mockUser = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockPharmacy = new Mock<IPharmacyRepository>(MockBehavior.Strict);
            _mockMedicine = new Mock<IMedicineRepository>(MockBehavior.Strict);
            _stockRequestService = new StockRequestService(_mockStockRequest.Object, _mockUser.Object, _mockPharmacy.Object, _mockMedicine.Object);

            _medicine = new Medicine()
            {
                Code = "ABC1234",
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",
                Stock = 10,
                Price = 500,
                Quantity = 0
            };

            _medicines = new List<Medicine>() { _medicine };

            _pharmacy = new Pharmacy()
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = _medicines
            };

            _user = new User()
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Pharmacy = new Pharmacy(),
                Role = RoleType.Administrator,
                Token = "ABD1234"
            };

            _petition = new PetitionStock()
            {
                MedicineCode = "ABC1234",
                Quantity = 5
            };

            _stockRequest = new StockRequest
            {
                Id = 1,
                Employee = _user,
                Pharmacy = _pharmacy,
                State = State.Active,
                Petitions = new List<PetitionStock>() { _petition }
            };

            _stockRequests = new List<StockRequest>() { _stockRequest };

            _stockRequestInactive = new StockRequest
            {
                Id = 1,
                Employee = _user,
                Pharmacy = _pharmacy,
                State = State.Inactive,
                Petitions = new List<PetitionStock>() { _petition }
            };

            _petitions = new List<Petition>() { _petition };

            _stockRequestInvalid = new StockRequest();



        }

        [TestMethod]
        public void InsertStockRequestTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockPharmacy.Setup(x => x.ExistsMedicines(_stockRequest.Petitions, _pharmacy.Name)).Returns(true);
            _mockStockRequest.Setup(x => x.InsertRequest(_stockRequest));
            _stockRequestService.InsertRequest(_stockRequest, _user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Alguna/s medicinas en la petición no existen en la farmacia. Revise los codigós de la/s medicina/s")]
        public void InsertInvalidStockRequestTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockPharmacy.Setup(x => x.ExistsMedicines(_stockRequest.Petitions, _pharmacy.Name)).Returns(false);
            _mockStockRequest.Setup(x => x.InsertRequest(_stockRequest));
            _stockRequestService.InsertRequest(_stockRequest, _user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        public void GetStockRequestsTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockStockRequest.Setup(x => x.GetRequestsByPharmacy(_pharmacy)).Returns(_stockRequests);
            _stockRequestService.GetRequests(_user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        public void AcceptStockRequestsTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockStockRequest.Setup(x => x.Exists(_stockRequest.Id)).Returns(true);
            _mockStockRequest.Setup(x => x.GetRequestById(It.IsAny<int>())).Returns(_stockRequest);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_petition.MedicineCode)).Returns(_medicine);
            _mockMedicine.Setup(x => x.Update(_medicine));
            _mockStockRequest.Setup(x => x.Update(_stockRequest));

            _stockRequestService.AcceptRequest(_stockRequest.Id, _user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "No existe la solicitud de stock ingresada en tu farmacia")]
        public void AcceptInvalidStockRequestsTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockStockRequest.Setup(x => x.Exists(_stockRequest.Id)).Returns(false);
            _mockStockRequest.Setup(x => x.GetRequestById(It.IsAny<int>())).Returns(_stockRequestInvalid);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_petition.MedicineCode)).Returns(_medicine);
            _mockMedicine.Setup(x => x.Update(_medicine));
            _mockStockRequest.Setup(x => x.Update(_stockRequest));

            _stockRequestService.AcceptRequest(_stockRequest.Id, _user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No existe la solicitud de stock ingresada en tu farmacia")]
        public void InvalidAcceptStockRequestsTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockStockRequest.Setup(x => x.Exists(_stockRequest.Id)).Returns(true);
            _mockStockRequest.Setup(x => x.GetRequestById(It.IsAny<int>())).Returns(_stockRequestInactive);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_petition.MedicineCode)).Returns(_medicine);
            _mockMedicine.Setup(x => x.Update(_medicine));
            _mockStockRequest.Setup(x => x.Update(_stockRequest));

            _stockRequestService.AcceptRequest(_stockRequest.Id, _user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        public void DeclineStockRequestsTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockStockRequest.Setup(x => x.GetRequestById(It.IsAny<int>())).Returns(_stockRequest);
            _mockStockRequest.Setup(x => x.Update(_stockRequest));

            _stockRequestService.DeclineRequest(_stockRequest.Id, _user.Token);
            _mockStockRequest.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "No existe la solicitud de stock ingresada en tu farmacia")]
        public void InvalidDeclineStockRequestsTest()
        {
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockStockRequest.Setup(x => x.GetRequestById(It.IsAny<int>())).Returns(_stockRequestInactive);
            _mockStockRequest.Setup(x => x.Update(_stockRequest));

            _stockRequestService.DeclineRequest(_stockRequest.Id, _user.Token);
            _mockStockRequest.VerifyAll();
        }
    }
}
