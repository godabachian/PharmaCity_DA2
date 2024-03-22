using System.Collections.Generic;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.Domain.DTO.IN;

namespace PharmaCity.IBusinessLogic
{
    public interface IMedicineService
    {
        MedicineDTO InsertMedicine(MedicineIN medicine, string token);
        IEnumerable<MedicineDTO> GetMedicines(string name, string pharmacy);
        void DeleteMedicine(string code);
        Medicine GetMedicineByCode(string code);
        PurchaseDTO BuyMedicines(Purchase purchease);
        void CheckAllMedicinesStock(Purchase purchease);
    }
}