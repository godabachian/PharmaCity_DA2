using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;

namespace PharmaCity.DataAccess
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private PharmaCityDbContext _context;

        public PharmacyRepository(PharmaCityDbContext context)
        {
            this._context = context;
        }

        public void InsertPharmacy(Pharmacy pharmacy)
        {
            _context.Pharmacies.Add(pharmacy);
            _context.SaveChanges();
        }

        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return _context.Pharmacies.ToList();
        }

        public void DeletePharmacy(Pharmacy pharmacy)
        {
            _context.Pharmacies.Remove(pharmacy);
            _context.SaveChanges();
        }

        public Pharmacy GetPharmacyByName(string pharmacyName)
        {
            return _context.Pharmacies.Include(pharmacy => pharmacy.Medicines).FirstOrDefault(p => p.Name.Equals(pharmacyName));
        }

        public bool Exists(string pharmacyName)
        {
            return _context.Pharmacies.Any(p => p.Name.Equals(pharmacyName));
        }

        public void Update(Pharmacy pharmacy)
        {
            _context.Update(pharmacy);
            _context.SaveChanges();
        }

        public bool ExistsMedicines(IEnumerable<Petition> petitions, string pharmacyName)
        {
            Pharmacy pharmacy = _context.Pharmacies.Include(pharmacy => pharmacy.Medicines).FirstOrDefault(p => p.Name.Equals(pharmacyName));

            bool exists = true;

            foreach (var petition in petitions)
            {
                exists = exists && pharmacy.Medicines.Any(medicine => medicine.Code == petition.MedicineCode.ToUpper());
            }

            return exists;
        }
    }
}