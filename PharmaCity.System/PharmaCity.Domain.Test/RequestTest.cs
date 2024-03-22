using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.Test
{
    [TestClass]
    public class RequestTest
    {
        private const int _Id = 1;
        private User _EmployeeTest = new User();
        private IEnumerable<PetitionStock> _PetitionsTest = new List<PetitionStock>();

        [TestMethod]
        public void CreateRequestTest()
        {
            StockRequest request = new StockRequest();
            Assert.IsNotNull(request);
        }


        [TestMethod]
        public void SetIdTest()
        {
            StockRequest request = new StockRequest();
            request.Id = _Id;
            Assert.AreEqual(request.Id, _Id);
        }

        [TestMethod]
        public void SetEmployeeTest()
        {
            StockRequest request = new StockRequest
            {
                Employee = _EmployeeTest
            };
            Assert.IsNotNull(request.Employee);
        }

        [TestMethod]
        public void SetPetitionsTest()
        {
            StockRequest request = new StockRequest
            {
                Petitions = _PetitionsTest
            };
            Assert.IsNotNull(request.Petitions);
        }
    }
}
