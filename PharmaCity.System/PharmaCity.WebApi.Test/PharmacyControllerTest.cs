using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using PharmaCity.WebApi.Controllers;

namespace PharmaCity.WebApi.Test
{
    [TestClass]
    public class PharmacyControllerTest
    {
        private Pharmacy _pharmacy = new Pharmacy();
        private PharmacyDTO _pharmacyDTO = new PharmacyDTO();
        private Mock<IPharmacyService> _mockedService;
        private PharmacyController _pharmacyApi;
        private IEnumerable<PharmacyDTO> _pharmaciesDTO;
        [TestInitialize]
        public void Setup()
        {
            _mockedService = new Mock<IPharmacyService>();
            _pharmacyApi = new PharmacyController(_mockedService.Object);
            _pharmacy = new Pharmacy
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = new List<Medicine>(),
            };
            _pharmacyDTO = new PharmacyDTO
            {
                Name = _pharmacy.Name,
                Direction = _pharmacy.Direction,
                Id = _pharmacy.Id
            };
            _pharmaciesDTO = new List<PharmacyDTO>
            {
                _pharmacyDTO
            };
        }

        [TestMethod]
        public void PostPharmacyTest()
        {
            _mockedService.Setup(x => x.InsertPharmacy(It.IsAny<Pharmacy>()));
            var result = _pharmacyApi.PostPharmacy(It.IsAny<Pharmacy>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Pharmacy;

            _mockedService.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetPharmacyTest()
        {
            _mockedService.Setup(x => x.GetPharmacies()).Returns(_pharmaciesDTO);
            var result = _pharmacyApi.GetPharmacy();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Pharmacy>;

            _mockedService.VerifyAll();
            Assert.AreEqual(200,statusCode);
        }
    }
}
