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
    public class PharmacyTest
    {
        private const int _Id = 1;
        private const string _NameTest = "PharmaCity";
        private const string _DirectionTest = "Av. 18 de Julio";
        IEnumerable<Medicine> _MedicinesTest = new List<Medicine>();

        [TestMethod]
        public void CreatePharmacyTest()
        {
            Pharmacy pharmacy = new Pharmacy();
            Assert.IsNotNull(pharmacy);
        }


        [TestMethod]
        public void SetIdTest()
        {
            Pharmacy pharmacy = new Pharmacy();
            pharmacy.Id = _Id;
            Assert.AreEqual(pharmacy.Id, _Id);
        }

        [TestMethod]
        public void SetNameTest()
        {
            Pharmacy pharmacy = new Pharmacy
            {
                Name = _NameTest
            };
            Assert.AreEqual(pharmacy.Name, _NameTest);
        }

        [TestMethod]
        public void SetDirectionTest()
        {
            Pharmacy pharmacy = new Pharmacy
            {
                Direction = _DirectionTest
            };
            Assert.AreEqual(pharmacy.Direction, _DirectionTest);
        }

        [TestMethod]
        public void SetMedicinesTest()
        {
            Pharmacy pharmacy = new Pharmacy
            {
                Medicines = _MedicinesTest
            };
            Assert.IsNotNull(pharmacy.Medicines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El nombre de la farmacia no puede ser nulo")]
        public void SetNullNameTest()
        {
            Pharmacy pharmacy = new Pharmacy
            {
                Name = null
            };
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El nombre de la farmacia debe tener el largo adecuado")]
        public void SetInvalidLengthNameTest()
        {
            Pharmacy pharmacy = new Pharmacy
            {
                Name = ""
            };
        }



    }
}
