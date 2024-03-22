using System.Collections.Generic;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;

namespace PharmaCity.IBusinessLogic
{
    public interface IPharmacyService
    {
        PharmacyDTO InsertPharmacy(Pharmacy pharmacy);
        IEnumerable<PharmacyDTO> GetPharmacies();
        void DeletePharmacy(string pharmacyName);
        Pharmacy GetPharmaciesByName(string pharmacyName);
    }
}