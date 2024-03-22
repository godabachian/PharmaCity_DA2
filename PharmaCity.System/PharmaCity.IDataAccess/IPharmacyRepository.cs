using System.Collections.Generic;
using PharmaCity.Domain;

namespace PharmaCity.IDataAccess
{
    public interface IPharmacyRepository
    {
        public void InsertPharmacy(Pharmacy pharmacy);
        IEnumerable<Pharmacy> GetPharmacies();
        void DeletePharmacy(Pharmacy pharmacy);
        Pharmacy GetPharmacyByName(string pharmacyName);
        bool Exists(string pharmacyName);
        void Update(Pharmacy pharmacy);
        bool ExistsMedicines(IEnumerable<Petition> petitions, string pharmacyName);
    }
}