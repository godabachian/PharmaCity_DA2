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
    public class PetitionTest
    {
        private const int _Id = 1;
        private const string _MedicineTest = "Paracetamol545";
        private const string _MedicineNameTest = "Paracetamol";
        private const int _QuantityTest = 100;
        private const int _InvalidQuantityTest = 0;

        [TestMethod]
        public void CreatePetitionTest()
        {
            Petition petition = new Petition();
            Assert.IsNotNull(petition);
        }

        [TestMethod]
        public void SetIdTest()
        {
            Petition petition = new Petition();
            petition.Id = _Id;
            Assert.AreEqual(petition.Id, _Id);
        }

        [TestMethod]
        public void SetMedicineTest()
        {
            Petition petition = new Petition
            {
                MedicineCode = _MedicineTest
            };
            Assert.IsNotNull(petition.MedicineCode);
        }

        [TestMethod]
        public void SetQuantityTest()
        {
            Petition petition = new Petition
            {
                Quantity = _QuantityTest
            };
            Assert.AreEqual(petition.Quantity, _QuantityTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La cantidad de la peticion debe ser mayor a 0")]
        public void SetInvalidQuantityTest()
        {
            Petition petition = new Petition
            {
                Quantity = _InvalidQuantityTest
            };
            Assert.AreEqual(petition.Quantity, _InvalidQuantityTest);
        }
    }
}
