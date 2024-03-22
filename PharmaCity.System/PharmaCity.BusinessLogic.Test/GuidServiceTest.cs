using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.BusinessLogic.Tools;
using PharmaCity.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic.Test
{
    [TestClass]
    public class GuidServiceTest
    {
        private GuidService _guidService;

        [TestInitialize]
        public void Setup()
        {
            _guidService = new GuidService();
        }

        [TestMethod]
        public void GuidTest()
        {
            Assert.IsNotNull(_guidService.NewGuid().ToString());
        }

        [TestMethod]
        public void GuidRandomCodeTest()
        {
            Assert.IsNotNull(_guidService.RandomCode());
        }

        [TestMethod]
        public void GuidRandomCodeMedicineTest()
        {
            Assert.IsNotNull(_guidService.RandomCodeMedicine());
        }
    }
}
