using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmaCity.BusinessLogic
{
    public class StockRequestService : IStockRequestService
    {
        private readonly IStockRequestRepository _stockRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IMedicineRepository _medicineRepository;

        public StockRequestService(IStockRequestRepository stockRequestRepository, IUserRepository userRepository, IPharmacyRepository pharmacyRepository, IMedicineRepository medicineRepository)
        {
            this._stockRequestRepository = stockRequestRepository;
            this._userRepository = userRepository;
            this._pharmacyRepository = pharmacyRepository;
            this._medicineRepository = medicineRepository;
        }

        public IEnumerable<StockRequestDTO> GetRequests(string token)
        {
            User userInDataBase = _userRepository.GetUserByToken(token);
            string pharmacyName = userInDataBase.Pharmacy.Name;
            Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);

            List<StockRequestDTO> requestDTOs = new List<StockRequestDTO>();

            foreach (var request in _stockRequestRepository.GetRequestsByPharmacy(pharmacyInDataBase))
            {
                requestDTOs.Add(GetRequestDTO(request));
            }

            return requestDTOs;
        }

        public StockRequestDTO InsertRequest(StockRequest stockRequest, string token)
        {
            User userInDataBase = _userRepository.GetUserByToken(token);
            string pharmacyName = userInDataBase.Pharmacy.Name;
            Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);

            if (_pharmacyRepository.ExistsMedicines(stockRequest.Petitions, pharmacyInDataBase.Name))
            {
                stockRequest.Pharmacy = pharmacyInDataBase;
                stockRequest.Employee = userInDataBase;
                stockRequest.State = State.Active;

                _stockRequestRepository.InsertRequest(stockRequest);

                return GetRequestDTO(stockRequest);
            }
            else
            {
                throw new InvalidOperationException("Alguna/s medicinas en la petición no existen en la farmacia. Revise los codigós de la/s medicina/s");
            }
        }

        private StockRequestDTO GetRequestDTO(StockRequest stockRequest)
        {
            StockRequestDTO stockRequestDto = new StockRequestDTO
            {
                Id = stockRequest.Id,
                EmployeeUserName = stockRequest.Employee.UserName,
                Petitions = stockRequest.Petitions
            };

            return stockRequestDto;
        }

        public void AcceptRequest(int stockRequestID, string token)
        {
            User userInDataBase = _userRepository.GetUserByToken(token);
            string pharmacyName = userInDataBase.Pharmacy.Name;
            Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);

            if (_stockRequestRepository.Exists(stockRequestID))
            {
                StockRequest stockRequestInDataBase = _stockRequestRepository.GetRequestById(stockRequestID);

                if (!(stockRequestInDataBase.Pharmacy.Name != pharmacyInDataBase.Name || stockRequestInDataBase.State == State.Inactive))
                {
                    foreach (var petition in stockRequestInDataBase.Petitions)
                    {
                        Medicine medicineInDataBase = _medicineRepository.GetMedicineByCode(petition.MedicineCode);
                        medicineInDataBase.Stock += petition.Quantity;
                        _medicineRepository.Update(medicineInDataBase);
                    }

                    stockRequestInDataBase.State = State.Inactive;
                    _stockRequestRepository.Update(stockRequestInDataBase);
                }
                else
                {
                    throw new InvalidOperationException("La solicitud de stock no es valida");
                }
            }
            else
            {
                throw new NullReferenceException("No existe la solicitud de stock ingresada en tu farmacia");
            }

        }

        public void DeclineRequest(int stockRequestID, string token)
        {
            User userInDataBase = _userRepository.GetUserByToken(token);
            string pharmacyName = userInDataBase.Pharmacy.Name;
            Pharmacy pharmacyInDataBase = _pharmacyRepository.GetPharmacyByName(pharmacyName);

            StockRequest stockRequestInDataBase = _stockRequestRepository.GetRequestById(stockRequestID);

            if (!(stockRequestInDataBase == null || stockRequestInDataBase.Pharmacy.Name != pharmacyInDataBase.Name || stockRequestInDataBase.State == State.Inactive))
            {
                stockRequestInDataBase.State = State.Inactive;
                _stockRequestRepository.Update(stockRequestInDataBase);
            }
            else
            {
                throw new NullReferenceException("No existe la solicitud de stock ingresada en tu farmacia");
            }
        }
    }
}