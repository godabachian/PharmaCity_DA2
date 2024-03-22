using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;

namespace PharmaCity.BusinessLogic
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IGuidService _guidService;
        private readonly IShoppingRepository _shoppingRepository;

        public MedicineService(IMedicineRepository medicineRepository, IUserRepository userRepository, IPharmacyRepository pharmacyRepository, IGuidService guidService, IShoppingRepository shoppingRepository)
        {
            this._medicineRepository = medicineRepository;
            this._userRepository = userRepository;
            this._pharmacyRepository = pharmacyRepository;
            this._guidService = guidService;
            this._shoppingRepository = shoppingRepository;
        }

        public MedicineDTO InsertMedicine(MedicineIN medicineIn, string token)
        {
            medicineIn.Code = _guidService.RandomCodeMedicine();
            medicineIn.Stock = 0;

            Medicine medicine = GetMedicineDomain(medicineIn);

            if(!_medicineRepository.Exists(medicine.Code))
            {
                Pharmacy pharmacyInDataBase = GetPharmacy(token);
                medicine.PharmacyName = pharmacyInDataBase.Name;
                AddMedicineToList(medicine, pharmacyInDataBase);
                _pharmacyRepository.Update(pharmacyInDataBase);

                return GetMedicineDTO(medicine);
            }
            else
            {
                throw new InvalidOperationException("Esta medicina ya esta en el sistema");
            }
        }

        private Medicine GetMedicineDomain(MedicineIN medicineIn)
        {
            Medicine medicine = new Medicine()
            {
                Code = medicineIn.Code,
                Name = medicineIn.Name,
                Symptoms = medicineIn.Symptoms,
                Presentation = medicineIn.Presentation,
                Unit = medicineIn.Unit,
                Receipt = medicineIn.Receipt,
                Quantity = Math.Abs(medicineIn.Quantity),
                Stock = medicineIn.Stock,
                Price = Math.Abs(medicineIn.Price)
            };

            return medicine;
        }

        private Pharmacy GetPharmacy(string token)
        {
            string pharmacyName = _userRepository.GetUserByToken(token).Pharmacy.Name;
            return _pharmacyRepository.GetPharmacyByName(pharmacyName);
        }

        private static void AddMedicineToList(Medicine medicine, Pharmacy pharmacyInDataBase)
        {
            List<Medicine> medicines = pharmacyInDataBase.Medicines.ToList();
            medicine.PharmacyName = pharmacyInDataBase.Name;
            medicines.Add(medicine);
            pharmacyInDataBase.Medicines = medicines;
        }

        private MedicineDTO GetMedicineDTO(Medicine medicine)
        {
            MedicineDTO medicineDTO = new MedicineDTO
            {
                Code = medicine.Code,
                Id = medicine.Id,
                Name = medicine.Name,
                Presentation = medicine.Presentation,
                Price = medicine.Price,
                Stock = medicine.Stock,
                Pharmacy = medicine.PharmacyName
            };
            return medicineDTO;
        }

        public IEnumerable<MedicineDTO> GetMedicines(string name, string pharmacy)
        {
            
            if (name == null && pharmacy == null)
            {
                return _medicineRepository.GetMedicines().Select(medicine => GetMedicineDTO(medicine)).ToList();
            }
            else
            {
                IList<Medicine> medicines = new List<Medicine>();
                if (pharmacy != null && name != null)
                {
                    medicines = _medicineRepository.GetMedicines().Where(x => x.Name.ToUpper().Contains(name.ToUpper()) && x.PharmacyName.ToUpper().Contains(pharmacy.ToUpper()) && x.Stock > 0).ToList();
                    return medicines.Select(x => GetMedicineDTO(x)).ToList();
                }
                else
                {
                    if (pharmacy == null)
                    {
                        medicines = _medicineRepository.GetMedicines().Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
                        return medicines.Select(x => GetMedicineDTO(x)).ToList();
                    }
                    else
                    {
                        medicines = _medicineRepository.GetMedicines().Where(x => x.PharmacyName.ToUpper().Contains(pharmacy.ToUpper()) && x.Stock > 0).ToList();
                        return medicines.Select(x => GetMedicineDTO(x)).ToList();
                    }
                }
            }
        }


        public void DeleteMedicine(string code)
        {
            if (_medicineRepository.Exists(code))
            {
                Medicine medicine = GetMedicineByCode(code);
                _medicineRepository.DeleteMedicine(medicine);
            }
            else
            {
                throw new NullReferenceException("La medicina no existe");
            }
        }

        public Medicine GetMedicineByCode(string code)
        {
            if (_medicineRepository.Exists(code))
            {
                Medicine medicine = _medicineRepository.GetMedicineByCode(code);
                return medicine;
            }
            else
            {
                throw new NullReferenceException("La medicina no existe");
            }
        }

        public PurchaseDTO BuyMedicines(Purchase purchase)
        {
            CheckAnyPurchase(purchase);

            if (CheckExistsAllMedicines(purchase))
            {
                if (CheckNoDoubleEntries(purchase))
                {
                    Medicine medicineInDataBase = _medicineRepository.GetMedicineByCode(purchase.Shopping.ToList()[0].MedicineCode);
                    Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(medicineInDataBase.PharmacyName);

                    CheckAllMedicinesStock(purchase);
                    SetPetitions(purchase);

                    purchase.State = State.Pending;
                    purchase.Code = _guidService.NewGuid();
                    purchase.Pharmacy = pharmacyInDataBase;
                    _shoppingRepository.InsertPurchase(purchase);

                    return GetPurchaseDTO(purchase);
                }
                else
                {
                    throw new InvalidOperationException("No se permite ingresar la misma medicina mas de una vez");
                }
            }
            else
            {
                throw new NullReferenceException("No existe alguno de los medicamentos en la farmacia");
            }
        }

        private void SetPetitions(Purchase purchase)
        {
            foreach (var petition in purchase.Shopping)
            {
                Medicine medicine = _medicineRepository.GetMedicineByCode(petition.MedicineCode);
                Pharmacy pharmacy = _pharmacyRepository.GetPharmacyByName(medicine.PharmacyName);

                petition.Pharmacy = pharmacy;
                petition.State = State.Pending;
            }
        }

        private void CheckAnyPurchase(Purchase purchase)
        {
            if (purchase.Shopping.Count() == 0)
            {
                throw new InvalidOperationException("No compraste ningun medicamento");
            }
        }

        private PurchaseDTO GetPurchaseDTO(Purchase purchease)
        {
            PurchaseDTO purchaseDto = new PurchaseDTO
            {
                Id = purchease.Id,
                PurchaseCode = purchease.Code,
                PharmacyName = purchease.Pharmacy.Name,
                Shopping = purchease.Shopping,
                State = purchease.State.ToString()
            };

            return purchaseDto;
        }

        private bool CheckExistsAllMedicines(Purchase purchease)
        {
            foreach (var petition in purchease.Shopping)
            {
                if (!_medicineRepository.Exists(petition.MedicineCode))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckNoDoubleEntries(Purchase purchase)
        {
            bool doubleEntry = true;
            List<PetitionBuy> shoppings = purchase.Shopping.ToList();
            foreach (PetitionBuy item in shoppings)
            {
                if (shoppings.Any(x => x.MedicineCode.Equals(item.MedicineCode)&&!(x.Equals(item))))
                {
                    doubleEntry = false;
                }
            }
            return doubleEntry;
        }
        public void CheckAllMedicinesStock(Purchase purchease)
        {
            foreach (var shop in purchease.Shopping)
            {
                Medicine medicine = _medicineRepository.GetMedicineByCode(shop.MedicineCode);

                if (medicine.Stock < shop.Quantity)
                {
                    throw new InvalidOperationException("No todas las medicinas tienen el stock solicitado");
                }
            }
        }
    }
}