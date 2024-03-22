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
    public class ExportServiceTest
    {
        private Mock<IMedicineRepository> _mockMedicine;
        private IExportService exportService;
        private IEnumerable<Medicine> _medicines;
        private Medicine _medicine;
        private string _mechanismName;

        [TestInitialize]
        public void Setup()
        {
            _mockMedicine = new Mock<IMedicineRepository>(MockBehavior.Strict);
            exportService = new ExportService(_mockMedicine.Object);

            _medicine = new Medicine()
            {
                Code = "RandomCode",
                PharmacyName = "PharmaCity",
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",
                Stock = 10,
                Price = 500,
                Quantity = 5
            };

            _medicines = new List<Medicine>() { _medicine };

            _mechanismName = "MechanismJSON";
        }

        [TestMethod]
        public void ExportTest()
        {
            _mockMedicine.Setup(x => x.GetMedicines()).Returns(_medicines);

            exportService.Export(_mechanismName);
            _mockMedicine.VerifyAll();
        }

        [TestMethod]
        public void GetExportsTest()
        {
            exportService.GetExporters();
            _mockMedicine.VerifyAll();
        }
    }
}
