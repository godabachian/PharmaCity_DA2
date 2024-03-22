using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;

namespace PharmaCity.DataAccess.Test
{
    [TestClass]
    public class MedicineRepositoryTest
    {
        private Medicine _medicine;
        private PharmaCityDbContext _context;
        private IMedicineRepository _medicineRepository;

        [TestInitialize]
        public void Setup()
        {
            _medicine = new Medicine
            {
                Code = "RandomCode",
                Name = "MedicineName",
                Symptoms = "Symptoms",
                Presentation = "Blister",
                Unit = "Grams",
                Receipt = "No",

            };

            _context = CreateContext();
            _medicineRepository = new MedicineRepository(_context);
        }

        [TestMethod]
        public void InsertMedicineTest()
        {
            _medicineRepository.InsertMedicine(_medicine);

            Medicine medicineInDataBase = _context.Medicines.Where<Medicine>(meds => meds.Id == (_medicine).Id)
                .AsNoTracking().FirstOrDefault();

            Assert.AreEqual(_medicine.Code, medicineInDataBase.Code);
        }

        [TestMethod]
        public void GetAllMedicineTest()
        {
            _context.Medicines.Add(_medicine);
            _context.SaveChanges();

            IEnumerable<Medicine> medicinesRepository = _medicineRepository.GetMedicines();

            Assert.AreEqual(medicinesRepository.Count(), 1);
        }

        [TestMethod]
        public void DeleteMedicineTest()
        {
            _context.Medicines.Add(_medicine);
            _context.SaveChanges();

            _medicineRepository.DeleteMedicine(_medicine);
            Medicine medicineInDataBase = _context.Medicines.Where<Medicine>(meds => meds.Id == (_medicine).Id)
                .AsNoTracking().FirstOrDefault();
            Assert.IsNull(medicineInDataBase);
        }

        [TestMethod]
        public void GetMedicineByCodeTest()
        {
            _context.Medicines.Add(_medicine);
            _context.SaveChanges();

            Medicine medicine = _medicineRepository.GetMedicineByCode(_medicine.Code);
            Assert.AreEqual(medicine.Code,_medicine.Code);
        }

        [TestMethod]
        public void ExistsMedicineTest()
        {
            _context.Medicines.Add(_medicine);
            _context.SaveChanges();

            Assert.IsTrue(_medicineRepository.Exists(_medicine.Code));
        }

        [TestMethod]
        public void UpdateMedicineTest()
        {
            _context.Medicines.Add(_medicine);
            _context.SaveChanges();

            _medicineRepository.Update(_medicine);

            Assert.IsTrue(_medicineRepository.Exists(_medicine.Code));
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
