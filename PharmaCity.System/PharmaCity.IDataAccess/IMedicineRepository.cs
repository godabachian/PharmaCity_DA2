using System.Collections.Generic;
using System.Reflection.Metadata;
using PharmaCity.Domain;

namespace PharmaCity.IDataAccess
{
    public interface IMedicineRepository
    {
        void InsertMedicine(Medicine medicine);
        IEnumerable<Medicine> GetMedicines();
        void DeleteMedicine(Medicine medicine);
        Medicine GetMedicineByCode(string medicineCode);
        bool Exists(string medicineCode);
        void Update(Medicine medicine);
    }
}