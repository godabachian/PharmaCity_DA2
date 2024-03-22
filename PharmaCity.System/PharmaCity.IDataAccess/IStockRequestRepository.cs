using PharmaCity.Domain;
using System.Collections.Generic;

namespace PharmaCity.IDataAccess
{
    public interface IStockRequestRepository
    {
        void InsertRequest(StockRequest stockRequest);
        IEnumerable<StockRequest> GetRequests();
        void DeleteRequest(StockRequest stockRequest);
        StockRequest GetRequestById(int id);
        bool Exists(int id);
        void Update(StockRequest stockRequest);
        IEnumerable<StockRequest> GetRequestsByPharmacy(Pharmacy pharmacyInDataBase);
    }
}