using Microsoft.EntityFrameworkCore;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.DataAccess
{
    public class ShoppingRepository : IShoppingRepository
    {
        private PharmaCityDbContext _dbContext;

        public ShoppingRepository(PharmaCityDbContext context)
        {
            this._dbContext = context;
        }

        public bool Exists(int id)
        {
            return _dbContext.Purchases.Any(purchase => purchase.Id == id);
        }

        public void Update(Purchase purchase)
        {
            _dbContext.Purchases.Update(purchase);
            _dbContext.SaveChanges();
        }

        public bool ExistsCode(string code)
        {
            return _dbContext.Purchases.Any(x => x.Code.Equals(code));
        }

        public IEnumerable<Purchase> GetShopping()
        {

            return _dbContext.Purchases.Where(x=>x.State != State.Inactive).Include(x => x.Shopping).ThenInclude(z=>z.Pharmacy).ToList();
        }

        public Purchase GetPurchaseById(int id)
        {
            return _dbContext.Purchases.Include(purchase => purchase.Shopping).ThenInclude(x => x.Pharmacy ).FirstOrDefault(purchase => purchase.Id == id);
        }

        public string GetShoppingStateByCode(string code)
        {
            return _dbContext.Purchases.FirstOrDefault(purchase => purchase.Code == code).State.ToString();
        }

        public void InsertPurchase(Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            _dbContext.SaveChanges();
        }
    }
}
