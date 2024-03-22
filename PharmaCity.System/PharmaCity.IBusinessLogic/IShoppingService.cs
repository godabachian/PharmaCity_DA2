using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using System.Collections.Generic;

namespace PharmaCity.IBusinessLogic
{
    public interface IShoppingService
    {
        IEnumerable<PetitionDTO> GetPetitions(string token);
        string GetPurchaseState(string code);

        IEnumerable<PurchaseDTO> GetShopping();

        void AcceptRequest(int purchaseId, string token);
        void DeclineRequest(int purchaseId, string token);

    }
}