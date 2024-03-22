using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.DataAccess.Test
{
    [TestClass]
    public class PetitionRepositoryTest
    {

        private Petition _petition;
        private PharmaCityDbContext _context;
        private IPetitionRepository _petitionRepository;

        private Pharmacy _pharmacy;

        [TestInitialize]
        public void Setup()
        {
            _pharmacy = new Pharmacy()
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = new List<Medicine>()
            };

            _petition = new Petition
            {
                MedicineCode = "Paracetamol5",
                Quantity = 5,
                Pharmacy = _pharmacy
            };

            _context = CreateContext();
            _petitionRepository = new PetitionRepository(_context);
        }

        [TestMethod]
        public void InsertPetitionTest()
        {
            _petitionRepository.InsertPetition(_petition);

            Petition petitionInDataBase = _context.Petitions.Where<Petition>(petition => petition.Id == _petition.Id)
                .AsNoTracking().FirstOrDefault();

            Assert.AreEqual(_petition.Id, petitionInDataBase.Id);
        }

        [TestMethod]
        public void GetAllPetitionTest()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();

            IEnumerable<Petition> petitionRepository = _petitionRepository.GetPetitions();

            Assert.AreEqual(petitionRepository.Count(), 1);
        }

        [TestMethod]
        public void GetAllPetitionBuyTest()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();

            IEnumerable<Petition> petitionRepository = _petitionRepository.GetPetitionsBuy(_pharmacy.Name);

            Assert.AreEqual(_petitionRepository.GetPetitions().Count(), 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();
            _petitionRepository.Update(_petition);
            Assert.IsTrue(_petitionRepository.Exists(_petition.Id));
        }

        [TestMethod]
        public void DeletePetitionTest()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();

            _petitionRepository.DeletePetition(_petition);

            Petition petitionInDataBase = _context.Petitions.Where<Petition>(petition => petition.Id == _petition.Id).AsNoTracking().FirstOrDefault();

            Assert.IsNull(petitionInDataBase);
        }

        [TestMethod]
        public void ExistsPetitionTest()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();

            Assert.IsTrue(_petitionRepository.Exists(_petition.Id));
        }

        [TestMethod]
        public void GetPetitionByIdTest()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();

            Assert.IsNotNull(_petitionRepository.GetPetitionById(_petition.Id));
        }

        [TestMethod]
        public void UpdatePetition()
        {
            _context.Petitions.Add(_petition);
            _context.SaveChanges();
            _context.Petitions.Update(_petition);

            Assert.IsTrue(_petitionRepository.Exists(_petition.Id));
        }

        private PharmaCityDbContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<PharmaCityDbContext>()
                .UseInMemoryDatabase("PetitionDb")
                .Options;

            var context = new PharmaCityDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
