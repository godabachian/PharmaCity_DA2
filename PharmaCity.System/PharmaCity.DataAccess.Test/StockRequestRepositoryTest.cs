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
    public class StockRequestRepositoryTest
    {
        private StockRequest _stockRequest;
        private User _user;
        private Pharmacy _pharmacy;
        private PharmaCityDbContext _context;
        private IStockRequestRepository _stockRequestRepository;

        [TestInitialize]
        public void Setup()
        {
            _pharmacy = new Pharmacy()
            {
                Name = "Pharmacy",
                Direction = "Random dir",
                Medicines = new List<Medicine>()
            };

            _user = new User
            {
                Email = "PharmaCity@gmail.com",
                UserName = "PharmaCityUY",
                Direction = "Av. 18 de Julio",
                Password = "Password!",
                Pharmacy = _pharmacy,
                RegisterDate = DateTime.Now,
                Token = "ABC1234",
                Role = RoleType.Administrator
            };

            _stockRequest = new StockRequest
            {
                Employee = _user,
                Pharmacy = _pharmacy,
                Petitions = new List<PetitionStock>(),
                State = State.Active
            };

            _context = CreateContext();
            _stockRequestRepository = new StockRequestRepository(_context);
        }

        [TestMethod]
        public void InsertStockRequestTest()
        {
            _stockRequestRepository.InsertRequest(_stockRequest);

            StockRequest stockRequestInDataBase = _context.StockRequests.Where<StockRequest>(request => request.Id == request.Id).AsNoTracking().FirstOrDefault();

            Assert.AreEqual(_stockRequest.Id, stockRequestInDataBase.Id);
        }

        [TestMethod]
        public void GetAllStockRequestsTest()
        {
            _context.StockRequests.Add(_stockRequest);
            _context.SaveChanges();

            IEnumerable<StockRequest> stockRequestRepository = _stockRequestRepository.GetRequests();

            Assert.AreEqual(stockRequestRepository.Count(), 1);
        }

        [TestMethod]
        public void GetAllStockRequestsByPharmacyTest()
        {
            _context.StockRequests.Add(_stockRequest);
            _context.SaveChanges();

            IEnumerable<StockRequest> stockRequestRepository = _stockRequestRepository.GetRequestsByPharmacy(_pharmacy);

            Assert.AreEqual(stockRequestRepository.Count(), 1);
        }

        [TestMethod]
        public void DeleteStockRequestTest()
        {
            _context.StockRequests.Add(_stockRequest);
            _context.SaveChanges();

            _stockRequestRepository.DeleteRequest(_stockRequest);

            StockRequest stockRequestInDataBase = _context.StockRequests.Where<StockRequest>(request => request.Id == request.Id).AsNoTracking().FirstOrDefault();

            Assert.IsNull(stockRequestInDataBase);
        }

        [TestMethod]
        public void GetStockRequestByIdTest()
        {
            _context.StockRequests.Add(_stockRequest);
            _context.SaveChanges();

            Assert.IsNotNull(_stockRequestRepository.GetRequestById(_stockRequest.Id));
        }

        [TestMethod]
        public void ExistsStockRequestTest()
        {
            _context.StockRequests.Add(_stockRequest);
            _context.SaveChanges();

            Assert.IsTrue(_stockRequestRepository.Exists(_stockRequest.Id));
        }

        private PharmaCityDbContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<PharmaCityDbContext>()
                .UseInMemoryDatabase("StockRequestDb")
                .Options;

            var context = new PharmaCityDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [TestMethod]
        public void UpdateStockTest()
        {
            _context.StockRequests.Add(_stockRequest);
            _context.SaveChanges();

            _stockRequestRepository.Update(_stockRequest);

            Assert.IsTrue(_stockRequestRepository.Exists(_stockRequest.Id));
        }
    }
}
