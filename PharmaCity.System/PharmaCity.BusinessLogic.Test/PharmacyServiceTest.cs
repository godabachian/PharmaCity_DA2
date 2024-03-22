using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;

namespace PharmaCity.BusinessLogic.Test
{
    [TestClass]

    public class PharmacyServiceTest
    {
        private Mock<IPharmacyRepository> _mockRepository;
        private IPharmacyService _pharmacyService;
        private Pharmacy _pharmacy;
        private Medicine _medicine;
        private IEnumerable<Pharmacy> _pharmacies;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IPharmacyRepository>(MockBehavior.Strict);
            _pharmacyService = new PharmacyService(_mockRepository.Object);

            _medicine = new Medicine
            {
                Code = "RandomCode",
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",

            };
            _pharmacy = new Pharmacy()
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = new List<Medicine>() { _medicine },
            };
            _pharmacies = new List<Pharmacy>()
            {
                _pharmacy
            };
        }

        [TestMethod]
        public void InsertPharmacyTest()
        {
            _mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            _mockRepository.Setup(x => x.InsertPharmacy(_pharmacy));
            _pharmacyService.InsertPharmacy(_pharmacy);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Esta farmacia ya existe")]
        public void InsertExistingPharmacyTest()
        {
            _mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _mockRepository.Setup(x => x.InsertPharmacy(_pharmacy));
            _pharmacyService.InsertPharmacy(_pharmacy);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetPharmaciesTest()
        {
            _mockRepository.Setup(x => x.GetPharmacies()).Returns(_pharmacies);
            _pharmacyService.GetPharmacies();
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void DeleteExistentPharmacyTest()
        {
            _mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _mockRepository.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockRepository.Setup(x => x.DeletePharmacy(_pharmacy));
            _pharmacyService.DeletePharmacy(_pharmacy.Name);
            _mockRepository.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),"Esta farmacia no existe")]
        public void DeleteNonExistentPharmacyTest()
        {
            _mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            _mockRepository.Setup(x => x.GetPharmacyByName(It.IsAny<string>())).Returns(_pharmacy);
            _mockRepository.Setup(x => x.DeletePharmacy(_pharmacy));
            _pharmacyService.DeletePharmacy(_pharmacy.Name);
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetExistentPharmacyByName()
        {
            _mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            _mockRepository.Setup(x => x.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            Pharmacy pharmacy = _pharmacyService.GetPharmaciesByName(_pharmacy.Name);
            _mockRepository.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),"No existe una farmacia con ese nombre")]
        public void GetNonExistentPharmacyByName()
        {
            _mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            _mockRepository.Setup(x => x.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            Pharmacy pharmacy = _pharmacyService.GetPharmaciesByName(_pharmacy.Name);
            _mockRepository.VerifyAll();
        }
    }
}
