using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using System.Collections.Generic;

namespace PharmaCity.IBusinessLogic
{
    public interface IStockRequestService
    {
        StockRequestDTO InsertRequest(StockRequest stockRequest, string token);
        IEnumerable<StockRequestDTO> GetRequests(string token);
        void AcceptRequest(int stockRequestID, string token);
        void DeclineRequest(int stockRequestID, string token);
    }
}