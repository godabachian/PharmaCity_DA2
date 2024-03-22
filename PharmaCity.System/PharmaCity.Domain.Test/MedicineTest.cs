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
    public class MedicineTest
    {
        private const int _Id = 1;
        private const string _TestCode = "thisisacode123";
        private const string _TestName = "DefaultName";
        private const string _TestSymptoms = "Acción analgésica";
        private const string _TestPresentation = "DefaultPresentation";
        private const int _TestQuantity = 5;
        private const string _TestUnit = "Mg";
        private const int _TestPrice = 5;
        private const int _TestStock = 5;
        private const string _Receipt = "No";
        private const string _LongTestName = "This name is too long for a medicine to have";
        private const string _LongTestUnit = "This is a long unit";

        [TestMethod]
        public void CreateMedicineTest()
        {
            Medicine medicine = new Medicine();
            Assert.IsNotNull(medicine);
        }

        [TestMethod]
        public void SetIdTest()
        {
            Medicine medicine = new Medicine();
            medicine.Id = _Id;
            Assert.AreEqual(medicine.Id, _Id);
        }

        [TestMethod]
        public void SetCodeTest()
        {
            Medicine medicine = new Medicine
            {
                Code = _TestCode
            };
            Assert.IsNotNull(medicine.Code);
        }

        [TestMethod]
        public void SetNameTest()
        {
            Medicine medicine = new Medicine
            {
                Name = _TestName
            };
            Assert.IsNotNull(medicine.Name);
        }

        [TestMethod]
        public void SetSymptomsTest()
        {
            Medicine medicine = new Medicine
            {
                Symptoms = _TestSymptoms
            };
            Assert.IsNotNull(medicine.Symptoms);
        }

        [TestMethod]
        public void SetPresentationTest()
        {
            Medicine medicine = new Medicine
            {
                Presentation = _TestPresentation
            };
            Assert.IsNotNull(medicine.Presentation);
        }

        [TestMethod]
        public void SetQuantityTest()
        {
            Medicine medicine = new Medicine
            {
                Quantity = _TestQuantity
            };
            Assert.IsNotNull(medicine.Quantity);
        }

        [TestMethod]
        public void SetUnitTest()
        {
            Medicine medicine = new Medicine
            {
                Unit = _TestUnit
            };
            Assert.IsNotNull(medicine.Unit);
        }

        [TestMethod]
        public void SetPriceTest()
        {
            Medicine medicine = new Medicine
            {
                Price = _TestPrice
            };
            Assert.IsNotNull(medicine.Price);
        }
        [TestMethod]
        public void SetStockTest()
        {
            Medicine medicine = new Medicine
            {
                Stock = _TestStock
            };
            Assert.IsNotNull(medicine.Stock);
        }

        [TestMethod]
        public void SetReceiptTest()
        {
            Medicine medicine = new Medicine
            {
                Receipt = _Receipt
            };
            Assert.IsNotNull(medicine.Receipt);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El codigo no debe ser nulo")]
        public void SetNullCodeTest()
        {
            Medicine medicine = new Medicine
            {
                Code = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El nombre no debe ser nulo")]
        public void SetNullNameTest()
        {
            Medicine medicine = new Medicine
            {
                Name = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El nombre debe")]
        public void SetLongNameTest()
        {
            Medicine medicine = new Medicine
            {
                Name = _LongTestName
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Los sintomas no deben ser nulo")]
        public void SetNullSymptomsTest()
        {
            Medicine medicine = new Medicine
            {
                Symptoms = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "La presentacion no puede ser nula")]
        public void SetNullPresentationTest()
        {
            Medicine medicine = new Medicine
            {
                Presentation = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La presentacion no puede ser tan larga")]
        public void SetLongPresentationTest()
        {
            Medicine medicine = new Medicine
            {
                Presentation = _LongTestName
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "La unidad de medida no puede ser nula")]
        public void SetNullUnitTest()
        {
            Medicine medicine = new Medicine
            {
                Unit = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La unidad de medida no puede ser tan larga")]
        public void SetLongUnitTest()
        {
            Medicine medicine = new Medicine
            {
                Unit = _LongTestUnit
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "La receta no puede ser nula")]
        public void SetNullReceiptTest()
        {
            Medicine medicine = new Medicine
            {
                Receipt = null
            };
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La receta es Yes o No")]
        public void SetLongReceiptTest()
        {
            Medicine medicine = new Medicine
            {
                Receipt = "Maybe"
            };
        }
    }
}
