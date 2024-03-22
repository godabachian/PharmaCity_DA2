using Microsoft.EntityFrameworkCore;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace PharmaCity.DataAccess
{
    public class StockRequestRepository : IStockRequestRepository
    {
        private PharmaCityDbContext _dbContext;

        public StockRequestRepository(PharmaCityDbContext context)
        {
            this._dbContext = context;
        }

        public IEnumerable<StockRequest> GetRequests()
        {
            return _dbContext.StockRequests.Include(request => request.Petitions).Include(request => request.Employee).Where(request => request.State == State.Active).ToList();
        }

        public void InsertRequest(StockRequest stockRequest)
        {
            _dbContext.StockRequests.Add(stockRequest);
            _dbContext.SaveChanges();
        }

        public void DeleteRequest(StockRequest stockRequest)
        {
            _dbContext.StockRequests.Remove(stockRequest);
            _dbContext.SaveChanges();
        }

        public StockRequest GetRequestById(int id)
        {
            return _dbContext.StockRequests.Include(request => request.Petitions).FirstOrDefault(request => request.Id == id);
        }

        public bool Exists(int id)
        {
            return _dbContext.StockRequests.Any(request => request.Id == id);
        }

        public void Update(StockRequest stockRequest)
        {
            _dbContext.StockRequests.Update(stockRequest);
            _dbContext.SaveChanges();
        }

        public IEnumerable<StockRequest> GetRequestsByPharmacy(Pharmacy pharmacy)
        {
            return _dbContext.StockRequests.Include(request => request.Petitions).Include(request => request.Employee).Where(request => request.Employee.Pharmacy == pharmacy && request.State == State.Active).ToList();
        }
    }
}