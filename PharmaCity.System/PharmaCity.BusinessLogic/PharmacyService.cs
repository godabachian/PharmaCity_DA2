using System;
using System.Collections.Generic;
using System.Linq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;

namespace PharmaCity.BusinessLogic
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IPharmacyRepository _pharmacyRepository;

        public PharmacyService(IPharmacyRepository pharmacyRepository)
        {
            this._pharmacyRepository = pharmacyRepository;
        }

        public PharmacyDTO InsertPharmacy(Pharmacy pharmacy)
        {
            if (!_pharmacyRepository.Exists(pharmacy.Name))
            {
                _pharmacyRepository.InsertPharmacy(pharmacy);
                return GetPharmacyDTO(pharmacy);
            }
            else
            {
                throw new InvalidOperationException("La farmacia ya existe");
            }
        }

        private PharmacyDTO GetPharmacyDTO(Pharmacy pharmacy)
        {
            PharmacyDTO pharmacyDTO = new PharmacyDTO
            {
                Id = pharmacy.Id,
                Name = pharmacy.Name,
                Direction = pharmacy.Direction,
            };
            return pharmacyDTO;
        }

        public IEnumerable<PharmacyDTO> GetPharmacies()
        {
            return _pharmacyRepository.GetPharmacies().Select(pharmacy => GetPharmacyDTO(pharmacy)).ToList();
        }

        public void DeletePharmacy(string pharmacyName)
        {
            if (_pharmacyRepository.Exists(pharmacyName))
            {
                Pharmacy pharmacy = _pharmacyRepository.GetPharmacyByName(pharmacyName);
                _pharmacyRepository.DeletePharmacy(pharmacy);
            }
            else
            {
                throw new NullReferenceException("No existe una farmacia con ese nombre");
            }
        }

        public Pharmacy GetPharmaciesByName(string pharmacyName)
        {
            if (_pharmacyRepository.Exists(pharmacyName))
            {
                Pharmacy pharmacy = _pharmacyRepository.GetPharmacyByName(pharmacyName);
                return pharmacy;
            }
            else
            {
                throw new NullReferenceException("No existe una farmacia con ese nombre");
            }
        }
    }
}