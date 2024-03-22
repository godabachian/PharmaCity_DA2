using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Controllers;
using PharmaCity.Domain.DTO.IN;

namespace PharmaCity.WebApi.Test
{
    [TestClass]
    public class MedicineControllerTest
    {
        private Medicine _medicine = new Medicine();
        private MedicineDTO _medicineDTO;
        private Mock<IMedicineService> _mockedicineService;
        private MedicineController _medApi;
        private IEnumerable<MedicineDTO> _medicinesDTO;
        private string _token;
        private Purchase _purchease;
        private PetitionBuy _petition;
        private Pharmacy _pharmacy;

        [TestInitialize]
        public void Setup()
        {
            _mockedicineService = new Mock<IMedicineService>();
            _medApi = new MedicineController(_mockedicineService.Object);

            _pharmacy = new Pharmacy()
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = new List<Medicine>()
            };

            _medicine = new Medicine
            {
                Code = "RandomCode",
                PharmacyName = _pharmacy.Name,
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",
                Price = 999,
                Stock = 10,
                Quantity = 10

            };
            _medicineDTO = new MedicineDTO
            {
                Id = _medicine.Id,
                Code = _medicine.Code,
                Name = _medicine.Name,
                Presentation = _medicine.Presentation,
                Price = _medicine.Price,
                Stock = _medicine.Stock
            };
            _medicinesDTO = new List<MedicineDTO>
            {
                _medicineDTO
            };

            _token = "ABC123";

            _petition = new PetitionBuy()
            {
                Id = 1,
                MedicineCode = _medicine.Code,
                Quantity = 1
            };

            _purchease = new Purchase()
            {
                Id = 1,
                Code = "ABCD1234",
                Shopping = new List<PetitionBuy> { _petition }
            };
        }

        [TestMethod]
        public void PostMedicineTest()
        {
            _mockedicineService.Setup(x => x.InsertMedicine(It.IsAny<MedicineIN>(), _token));
            var result = _medApi.PostMedicine(It.IsAny<MedicineIN>(), _token);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Medicine;

            _mockedicineService.VerifyAll();
            Assert.AreEqual(200,statusCode);
        }

        [TestMethod]
        public void GetMedicineTest()
        {
            _mockedicineService.Setup(x => x.GetMedicines(null,null)).Returns(_medicinesDTO);
            var result = _medApi.GetMedicines(null,null);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Medicine>;

            _mockedicineService.VerifyAll();
            Assert.AreEqual(200,statusCode);
        }

        [TestMethod]
        public void PostBuyMedicineTest()
        {
            _mockedicineService.Setup(x => x.BuyMedicines(_purchease));
            var result = _medApi.BuyMedicines(_purchease);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Medicine>;

            _mockedicineService.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void DeleteMedicineTest()
        {
            _mockedicineService.Setup(x => x.DeleteMedicine(It.IsAny<string>()));
            var result = _medApi.DeleteMedicine(It.IsAny<string>());
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as string;

            _mockedicineService.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }
    }
}
