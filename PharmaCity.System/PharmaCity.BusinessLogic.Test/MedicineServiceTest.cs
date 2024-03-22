using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;

namespace PharmaCity.BusinessLogic.Test
{
    [TestClass]
    public class MedicineServiceTest
    {
        private IMedicineService _medicineService;
        private Mock<IMedicineRepository> _mockMedicine;
        private Mock<IUserRepository> _mockUser;
        private Mock<IPharmacyRepository> _mockPharmacy;
        private Mock<IGuidService> _mockGuid;
        private Mock<IShoppingRepository> _mockShopping;
        private Medicine _medicine;
        private MedicineIN _medicineIn;
        private IEnumerable<Medicine> medicines;
        private User _user;
        private Pharmacy _pharmacy;
        private Purchase _purchase;
        private Purchase _purchaseDoubleMedicine;
        private PetitionBuy _petition;
        private PetitionBuy _petitionTWO;

        private Medicine _medicineInvalid;
        private Purchase _purchaseInvalid;

        private string _nameMedicineFilter;
        private string _pharmacyNameFilter;

        [TestInitialize]
        public void Setup()
        {
            _mockMedicine = new Mock<IMedicineRepository>(MockBehavior.Strict);
            _mockUser = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockPharmacy = new Mock<IPharmacyRepository>(MockBehavior.Strict);
            _mockGuid = new Mock<IGuidService>(MockBehavior.Strict);
            _mockShopping = new Mock<IShoppingRepository>(MockBehavior.Strict);
            _medicineService = new MedicineService(_mockMedicine.Object, _mockUser.Object, _mockPharmacy.Object, _mockGuid.Object, _mockShopping.Object);

            _pharmacy = new Pharmacy()
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = new List<Medicine>()
            };

            _medicine = new Medicine()
            {
                Code = "RandomCode",
                PharmacyName = _pharmacy.Name,
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",
                Stock = 10,
                Price = 500,
                Quantity = 5
            };

            _medicineInvalid = new Medicine()
            {
                Code = "RandomCode",
                PharmacyName = _pharmacy.Name,
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",
                Stock = 0,
                Price = 500,
                Quantity = 0
            };

            medicines = new List<Medicine>()
            {
                _medicine
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

            _petition = new PetitionBuy()
            {
                MedicineCode = _medicine.Code,
                Quantity = 1,
                State = State.Pending
            };

            _petitionTWO = new PetitionBuy()
            {
                MedicineCode = _medicine.Code,
                Quantity = 1
            };

            _purchase = new Purchase()
            {
                Id = 1,
                Code = "ABCD1234",
                Pharmacy = _pharmacy,
                Shopping = new List<PetitionBuy> { _petition },
                State = State.Pending
            };

            _purchaseDoubleMedicine = new Purchase()
            {
                Id = 1,
                Code = "ABCD1234",
                Pharmacy = _pharmacy,
                Shopping = new List<PetitionBuy> { _petition, _petitionTWO }
            };

            _purchaseInvalid = new Purchase()
            {
                Code = "ABCD1234",
                Shopping = new List<PetitionBuy>()
            };

            _nameMedicineFilter = _medicine.Name;
            _pharmacyNameFilter = _pharmacy.Name;

            _medicineIn = new MedicineIN()
            {
                Code = "RandomCode",
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",
                Stock = 10,
                Price = 500,
                Quantity = 5
            };
        }

        [TestMethod]
        public void InsertMedicineTest()
        {
            _mockGuid.Setup(x => x.RandomCodeMedicine()).Returns("RandomCode");
            _mockMedicine.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            _mockUser.Setup(x => x.GetUserByToken(_user.Token)).Returns(_user);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockPharmacy.Setup(x => x.Update(_pharmacy));
            _medicineService.InsertMedicine(_medicineIn, _user.Token);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "This medicine is already in the system")]
        public void InsertExistingMedicineTest()
        {
            _mockGuid.Setup(x => x.RandomCodeMedicine()).Returns("RandomCode");
            _mockMedicine.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _mockMedicine.Setup(x => x.InsertMedicine(_medicine));
            _medicineService.InsertMedicine(_medicineIn, _user.Token);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void GetMedicinesTest()
        {
            _mockMedicine.Setup(x => x.GetMedicines()).Returns(medicines);
            _medicineService.GetMedicines(null,null);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void GetMedicinesByNameTest()
        {
            _mockMedicine.Setup(x => x.GetMedicines()).Returns(medicines);
            _medicineService.GetMedicines(_nameMedicineFilter, null);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void GetMedicinesByNamePharmacyTest()
        {
            _mockMedicine.Setup(x => x.GetMedicines()).Returns(medicines);
            _medicineService.GetMedicines(null, _pharmacyNameFilter);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void GetMedicinesByNamePharmacyAndNameMedicineTest()
        {
            _mockMedicine.Setup(x => x.GetMedicines()).Returns(medicines);
            _medicineService.GetMedicines(_nameMedicineFilter, _pharmacyNameFilter);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void DeleteExistentMedicineTest()
        {
            _mockMedicine.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_medicine.Code)).Returns(_medicine);
            _mockMedicine.Setup(x => x.DeleteMedicine(_medicine));
            _medicineService.DeleteMedicine(_medicine.Code);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "This medicine does not exist")]
        public void DeleteNonExistentMedicine()
        {
            _mockMedicine.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_medicine.Code)).Returns(_medicine);
            _mockMedicine.Setup(x => x.DeleteMedicine(_medicine));
            _medicineService.DeleteMedicine(_medicine.Code);
            _mockMedicine.VerifyAll();
        }
        [TestMethod]
        public void GetExistentMedByCode()
        {
            _mockMedicine.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_medicine.Code)).Returns(_medicine);
            Medicine meds = _medicineService.GetMedicineByCode(_medicine.Code);
            _mockMedicine.VerifyAll();

        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "This medicine does not exist")]
        public void GetNonExistentMedByCode()
        {
            _mockMedicine.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_medicine.Code)).Returns(_medicine);
            Medicine meds = _medicineService.GetMedicineByCode(_medicine.Code);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void BuyMedicinesTest()
        {
            _mockMedicine.Setup(x => x.Exists(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockPharmacy.Setup(x => x.ExistsMedicines(_purchase.Shopping,_medicine.PharmacyName)).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_medicine.PharmacyName)).Returns(_pharmacy);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockGuid.Setup(x => x.NewGuid()).Returns("ABCD1234");
            _mockShopping.Setup(x => x.InsertPurchase(_purchase));

            _medicineService.BuyMedicines(_purchase);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "No compraste ningun medicamento")]
        public void BuyInvalidMedicineTest()
        {
            _mockMedicine.Setup(x => x.Exists(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(false);

            _medicineService.BuyMedicines(_purchase);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No todas las medicinas tienen el stock solicitado")]
        public void BuyInvalidStockMedicinesTest()
        {
            _mockMedicine.Setup(x => x.Exists(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockPharmacy.Setup(x => x.ExistsMedicines(_purchase.Shopping, _medicine.PharmacyName)).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_medicine.PharmacyName)).Returns(_pharmacy);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicineInvalid);
            _mockMedicine.Setup(x => x.Update(_medicine));

            _medicineService.BuyMedicines(_purchase);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No compraste ningun medicamento")]
        public void BuyInvalidPetitionsMedicinesTest()
        {
            _medicineService.BuyMedicines(_purchaseInvalid);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "No se permite ingresar la misma medicina mas de una vez")]
        public void BuyInvalidMedicinesTest()
        {
            _mockMedicine.Setup(x => x.Exists(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(true);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockPharmacy.Setup(x => x.ExistsMedicines(_purchaseDoubleMedicine.Shopping, _medicine.PharmacyName)).Returns(true);


            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_medicine.PharmacyName)).Returns(_pharmacy);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockMedicine.Setup(x => x.GetMedicineByCode(_purchase.Shopping.ToList()[0].MedicineCode)).Returns(_medicine);
            _mockMedicine.Setup(x => x.Update(_medicine));
            _mockGuid.Setup(x => x.NewGuid()).Returns("ABCD1234");
            _mockShopping.Setup(x => x.InsertPurchase(_purchaseDoubleMedicine));

            _medicineService.BuyMedicines(_purchaseDoubleMedicine);
            _mockMedicine.VerifyAll();
        }
    }
}