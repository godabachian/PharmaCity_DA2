using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
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
    public class ExportControllerTest
    {
        private Mock<IExportService> _mock;
        private ExportController _api;
        private string _mechanismName;
        private IEnumerable<string> _exporters;

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IExportService>(MockBehavior.Strict);
            _api = new ExportController(_mock.Object);

            _mechanismName = "MechanismJSON";
            _exporters = new List<string>();
        }

        [TestMethod]
        public void ExportTest()
        {
            _mock.Setup(x => x.Export(_mechanismName));

            var result = _api.ExportMedicines(_mechanismName);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Medicine;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetExportersTest()
        {
            _mock.Setup(x => x.GetExporters()).Returns(_exporters);

            var result = _api.GetExporters();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IEnumerable<Medicine>;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }
    }
}
