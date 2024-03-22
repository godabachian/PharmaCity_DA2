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
    public class ShoppingRepositoryTest
    {
        private Purchase _purchase;
        private PetitionBuy _petition;
        private PharmaCityDbContext _context;
        private IShoppingRepository _shoppingRepository;

        [TestInitialize]
        public void Setup()
        {
            _petition = new PetitionBuy
            {
                MedicineCode = "PAR15",
                Quantity = 5,
                State = State.Pending
            };

            _purchase = new Purchase
            {
                Code = "ABC123",
                Shopping = new List<PetitionBuy> { _petition },
                State = State.Pending
            };

            _context = CreateContext();
            _shoppingRepository = new ShoppingRepository(_context);
        }

        [TestMethod]
        public void InsertInvitationTest()
        {
            _shoppingRepository.InsertPurchase(_purchase);

            Purchase purchaseInDataBase = _context.Purchases.Where<Purchase>(purchase => purchase.Id == purchase.Id).AsNoTracking().FirstOrDefault();

            Assert.AreEqual(_purchase.Code, purchaseInDataBase.Code);
        }

        [TestMethod]
        public void GetAllInvitationTest()
        {
            _context.Purchases.Add(_purchase);
            _context.SaveChanges();

            IEnumerable<Purchase> shoppingRepository = _shoppingRepository.GetShopping();

            Assert.AreEqual(shoppingRepository.Count(), 1);
        }

        [TestMethod]
        public void ExistsPurchaseTest()
        {
            _context.Purchases.Add(_purchase);
            _context.SaveChanges();

            Assert.IsTrue(_shoppingRepository.ExistsCode(_purchase.Code));
        }

        [TestMethod]
        public void GetStatePurchaseTest()
        {
            _context.Purchases.Add(_purchase);
            _context.SaveChanges();

            Assert.AreEqual(_shoppingRepository.GetShoppingStateByCode(_purchase.Code), _purchase.State.ToString());
        }

        [TestMethod]
        public void GetPurchaseByIdTest()
        {
            _context.Purchases.Add(_purchase);
            _context.SaveChanges();
            Purchase comparee = _shoppingRepository.GetPurchaseById(_purchase.Id);
            Assert.AreEqual(comparee, _purchase);
        }
        [TestMethod]
        public void UpdatePurchaseTest()
        {
            _context.Purchases.Add(_purchase);
            _context.SaveChanges();
            _shoppingRepository.Update(_purchase);
            Assert.IsTrue(_shoppingRepository.ExistsCode(_purchase.Code));
        }

        [TestMethod]
        public void ExistsTest()
        {
            _context.Purchases.Add(_purchase);
            _context.SaveChanges();
            Assert.IsTrue(_shoppingRepository.Exists(_purchase.Id));
        }

        private PharmaCityDbContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<PharmaCityDbContext>()
                .UseInMemoryDatabase("PurchaseDb")
                .Options;

            var context = new PharmaCityDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
