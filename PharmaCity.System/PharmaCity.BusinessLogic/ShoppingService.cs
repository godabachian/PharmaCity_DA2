using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic
{
    public class ShoppingService : IShoppingService
    {
        private readonly IPetitionRepository _petitionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingRepository _shoppingRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicineService _medicineService;

        public ShoppingService(IPetitionRepository petitionRepository, IUserRepository userRepository, IShoppingRepository shoppingRepository, IPharmacyRepository pharmacyRepository, IMedicineRepository medicineRepository, IMedicineService medicineService)
        {
            this._petitionRepository = petitionRepository;
            this._userRepository = userRepository;
            this._shoppingRepository = shoppingRepository;
            this._pharmacyRepository = pharmacyRepository;
            this._medicineRepository = medicineRepository;
            this._medicineService = medicineService;
        }

        public IEnumerable<PetitionDTO> GetPetitions(string token)
        {
            User user = _userRepository.GetUserByToken(token);
            string pharmacyName = user.Pharmacy.Name;

            return GetPetitionsByPharmacy(pharmacyName);
        }

        public IEnumerable<PurchaseDTO> GetShopping()
        {
            return GetShoppingDTO(_shoppingRepository.GetShopping());
        }

        private IEnumerable<PurchaseDTO> GetShoppingDTO(IEnumerable<Purchase> shopping)
        {
            IList<PurchaseDTO> shoppingDTO = new List<PurchaseDTO>();

            foreach (var shop in shopping)
            {
                shoppingDTO.Add(GetPurchaseDTO(shop));
            }

            return shoppingDTO;
        }

        private PurchaseDTO GetPurchaseDTO(Purchase shop)
        {
            PurchaseDTO purchaseDTO = new PurchaseDTO
            {
                Id = shop.Id,
                PurchaseCode = shop.Code,
                PharmacyName = shop.Pharmacy.Name,
                State = shop.State.ToString(),
                Shopping = shop.Shopping
            };

            return purchaseDTO;
        }

        private IEnumerable<PetitionDTO> GetPetitionsByPharmacy(string pharmacyName)
        {
            IList<PetitionDTO> petitionsDtos = new List<PetitionDTO>();
            Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);

            foreach (var petition in _petitionRepository.GetPetitionsBuy(pharmacyName))
            {
                petitionsDtos.Add(GetPetitionDTO(petition));
            }

            return petitionsDtos;
        }

        private PetitionDTO GetPetitionDTO(Petition petition)
        {
            PetitionDTO petitionDTO = new PetitionDTO
            {
                Id = petition.Id,
                MedicineCode = petition.MedicineCode,
                PharmacyName = petition.Pharmacy.Name,
                Quantity = petition.Quantity,
                State = petition.State.ToString()
            };

            return petitionDTO;
        }

        public string GetPurchaseState(string code)
        {
            CheckExistsPurchase(code);

            return _shoppingRepository.GetShoppingStateByCode(code);
        }

        public void AcceptRequest(int purchaseID, string token)
        {
            if (_shoppingRepository.Exists(purchaseID))
            {
                Purchase purchaseInDataBase = _shoppingRepository.GetPurchaseById(purchaseID);
                if (purchaseInDataBase.State != State.Active)
                {
                    User userInDataBase = _userRepository.GetUserByToken(token);
                    string pharmacyName = userInDataBase.Pharmacy.Name; 
                    Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);
                    DeactivatePetitions(purchaseInDataBase.Shopping.Where(X => X.Pharmacy.Equals(pharmacyInDataBase)));
                    if (purchaseInDataBase.Shopping.All(x => x.State == State.Active))
                    {
                        _medicineService.CheckAllMedicinesStock(purchaseInDataBase);
                        DecreaseStock(purchaseInDataBase);
                        purchaseInDataBase.State = State.Active;
                        _shoppingRepository.Update(purchaseInDataBase);
                    }
                }
                else
                {
                    throw new InvalidOperationException("La solicitud de compra ya fue procesada");
                }
            }
            else
            {
                throw new NullReferenceException("No existe la solicitud de compra ingresada");
            }

        }

        private void DeactivatePetitions(IEnumerable<Petition> purchase)
        {

            foreach (var petition in purchase)
            {
                petition.State = State.Active;
                _petitionRepository.Update(petition);
            }
        }

        private void DecreaseStock(Purchase purchaseInDataBase)
        {
            foreach (var petition in purchaseInDataBase.Shopping)
            {
                Medicine medicineInDataBase = _medicineRepository.GetMedicineByCode(petition.MedicineCode);
                medicineInDataBase.Stock -= petition.Quantity;
                _medicineRepository.Update(medicineInDataBase);
            }
        }

        public void DeclineRequest(int purchaseId, string token)
        {
            User userInDataBase = _userRepository.GetUserByToken(token);
            string pharmacyName = userInDataBase.Pharmacy.Name;
            Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);
            Purchase purchaseInDataBase = _shoppingRepository.GetPurchaseById(purchaseId);
            if (purchaseInDataBase != null 
                && purchaseInDataBase.State == State.Pending
                && purchaseInDataBase.Shopping.Any(x=>x.Pharmacy.Equals(pharmacyInDataBase)))
            {
                DeactivatePetitions(purchaseInDataBase.Shopping);
                purchaseInDataBase.State = State.Inactive;
                _shoppingRepository.Update(purchaseInDataBase);
            }
            else
            {
                throw new NullReferenceException("No existe la solicitud de compra ingresada en tu farmacia");
            }
        }

        private void CheckExistsPurchase(string code)
        {
            if (!(_shoppingRepository.ExistsCode(code)))
            {
                throw new InvalidOperationException("No existe una compra con el codigo ingresado.");
            }
        }
    }
}
