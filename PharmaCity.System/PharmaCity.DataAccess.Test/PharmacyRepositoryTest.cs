using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;

namespace PharmaCity.DataAccess.Test
{

    [TestClass]
    public class PharmacyRepositoryTest
    {
        private Pharmacy _pharmacy;
        private Medicine _medicine;
        private StockRequest _stockRequest;
        private PetitionStock _petition;
        private PharmaCityDbContext _context;
        private IPharmacyRepository _pharmacyRepository;

        [TestInitialize]
        public void Setup()
        {
            _medicine = new Medicine
            {
                Code = "ABC123",
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
                Medicines = new List<Medicine>(){_medicine},
            };
            _context = CreateContext();
            _pharmacyRepository = new PharmacyRepository(_context);

            _petition = new PetitionStock()
            {
                MedicineCode = "ABC123",
                Quantity = 5
            };

            _stockRequest = new StockRequest
            {
                Employee = new User(),
                Pharmacy = _pharmacy,
                State = State.Inactive,
                Petitions = new List<PetitionStock>() { _petition }
            };

            _petition = new PetitionStock()
            {
                MedicineCode = "RandomCode",
                Quantity = 5
            };
        }

        [TestMethod]
        public void InsertPharmacyTest()
        {
            _pharmacyRepository.InsertPharmacy(_pharmacy);
            Pharmacy pharmacyInDataBase = _context.Pharmacies.Where<Pharmacy>(p => p.Id == (_pharmacy).Id)
                .AsNoTracking().FirstOrDefault();
            Assert.AreEqual(_pharmacy.Name, pharmacyInDataBase.Name);
        }

        [TestMethod]
        public void GetAllPharmaciesTest()
        {
            _context.Pharmacies.Add(_pharmacy);
            _context.SaveChanges();

            IEnumerable<Pharmacy> pharmaciesRepository = _pharmacyRepository.GetPharmacies();

            Assert.AreEqual(1, pharmaciesRepository.Count());

        }
        [TestMethod]
        public void DeletePharmacyTest()
        {
            _context.Pharmacies.Add(_pharmacy);
            _context.SaveChanges();

            _pharmacyRepository.DeletePharmacy(_pharmacy);
            Pharmacy pharmacyInDataBase = _context.Pharmacies.Where<Pharmacy>(p => p.Id == (_pharmacy).Id)
                .AsNoTracking().FirstOrDefault();
            Assert.IsNull(pharmacyInDataBase);
        }

        [TestMethod]
        public void GetPharmacyByNameTest()
        {
            _context.Pharmacies.Add(_pharmacy);
            _context.SaveChanges();

            Pharmacy pharmacy = _pharmacyRepository.GetPharmacyByName(_pharmacy.Name);
            Assert.AreEqual(pharmacy.Name,_pharmacy.Name);
        }

        [TestMethod]
        public void ExistsPharmacyTest()
        {
            _context.Pharmacies.Add(_pharmacy);
            _context.SaveChanges();

            Assert.IsTrue(_pharmacyRepository.Exists(_pharmacy.Name));
        }

        [TestMethod]
        public void UpdatePharmacyTest()
        {
            _context.Pharmacies.Add(_pharmacy);
            _context.SaveChanges();

            _pharmacyRepository.Update(_pharmacy);

            Assert.IsTrue(_pharmacyRepository.Exists(_pharmacy.Name));
        }

        [TestMethod]
        public void ExistsMedicinesPharmacyTest()
        {
            _context.Pharmacies.Add(_pharmacy);
            _context.SaveChanges();

            bool exists = _pharmacyRepository.ExistsMedicines(_stockRequest.Petitions, _pharmacy.Name);

            Assert.IsTrue(exists);
        }

        private PharmaCityDbContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<PharmaCityDbContext>()
                .UseInMemoryDatabase("MedicineDb")
                .Options;
            var context = new PharmaCityDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
