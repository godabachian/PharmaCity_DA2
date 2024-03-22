using System.Collections.Generic;
using System.Linq;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;

namespace PharmaCity.DataAccess
{
    public class MedicineRepository : IMedicineRepository
    {
        private PharmaCityDbContext _dbContext;

        public MedicineRepository(PharmaCityDbContext context)
        {
            this._dbContext = context;
        }

        public void InsertMedicine(Medicine medicine)
        {
            _dbContext.Medicines.Add(medicine);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            return _dbContext.Medicines.ToList();
        }

        public void DeleteMedicine(Medicine medicine)
        {
            _dbContext.Medicines.Remove(medicine);
            _dbContext.SaveChanges();
        }

        public Medicine GetMedicineByCode(string code)
        {
            return _dbContext.Medicines.FirstOrDefault(med => med.Code == code);
        }

        public bool Exists(string medicineCode)
        {
            return _dbContext.Medicines.Any(med => med.Code == medicineCode);
        }

        public void Update(Medicine medicine)
        {
            _dbContext.Medicines.Update(medicine);
            _dbContext.SaveChanges();
        }
    }
}