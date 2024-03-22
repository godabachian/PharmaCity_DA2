using PharmaCity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.IDataAccess
{
    public interface IShoppingRepository
    {
        void InsertPurchase(Purchase purchase);
        IEnumerable<Purchase> GetShopping();
        string GetShoppingStateByCode(string code);
        Purchase GetPurchaseById(int purchaseId);
        bool Exists(int id);
        void Update(Purchase purchase);
        bool ExistsCode(string code);
    }
}
