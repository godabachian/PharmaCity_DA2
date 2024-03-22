using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;

namespace PharmaCity.BusinessLogic
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IGuidService _guidService;

        public InvitationService(IInvitationRepository invitationRepository, IPharmacyRepository pharmacyRepository, IGuidService guidService)
        {
            this._invitationRepository = invitationRepository;
            this._pharmacyRepository = pharmacyRepository;
            this._guidService = guidService;
        }

        public InvitationDTO InsertInvitation(InvitationIN invitation)
        {
            CheckExistsInvitation(invitation);
            CheckCorrectRole(invitation.Role);
            CheckCorrectPharmacyAndRole(invitation.PharmacyName, invitation.Role);

            Invitation invitationDomain = GetInvitationDomain(invitation);

            invitationDomain.State = State.Active;
            invitationDomain.Code = _guidService.RandomCode();

            if (invitationDomain.Role != 0)
            {
                invitationDomain.Pharmacy = _pharmacyRepository.GetPharmacyByName(invitation.PharmacyName);
            }
            _invitationRepository.InsertInvitation(invitationDomain);

            return GetInvitationDTO(invitationDomain);
        }

        private Invitation GetInvitationDomain(InvitationIN invitation)
        {
            Invitation invitationDomain = new Invitation()
            {
                UserName = invitation.UserName,
                Role = invitation.Role
            };

            return invitationDomain;
        }

        private void CheckCorrectPharmacyAndRole(string pharmacyName, RoleType role)
        {
            if (!(_pharmacyRepository.Exists(pharmacyName) || role == 0))
            {
                throw new NullReferenceException("La farmacia no existe");
            }
        }

        private void CheckExistsInvitation(InvitationIN invitation)
        {
            if (_invitationRepository.Exists(invitation.UserName))
            {
                throw new InvalidOperationException("La invitación ya existe");
            }
        }

        private void CheckCorrectRole(RoleType role)
        {
            if (!(role is RoleType.Owner or RoleType.Administrator or RoleType.Employee))
            {
                throw new NullReferenceException("No existe el rol ingresado");
            }
        }

        private InvitationDTO GetInvitationDTO(Invitation invitation)
        {
            InvitationDTO invitationDto = new InvitationDTO();

            invitationDto.Id = invitation.Id;
            invitationDto.UserName = invitation.UserName;
            invitationDto.Role = invitation.Role.ToString();
            invitationDto.Code = invitation.Code;
            invitationDto.State = GetStateDTO(invitation);
            invitationDto.PharmacyName = GetPharmacyDTO(invitation);

            return invitationDto;
        }

        private string GetPharmacyDTO(Invitation invitation)
        {
            string PharmacyDTO;

            if (invitation.Pharmacy != null)
            {
                PharmacyDTO = invitation.Pharmacy.Name;
            }
            else
            {
                PharmacyDTO = "(Admin no posee farmacia)";
            }

            return PharmacyDTO;
        }

        private string GetStateDTO(Invitation invitation)
        {
            string StateDTO;

            if (invitation.State == State.Active)
            {
                StateDTO = "Disponible";
            }
            else
            {
                StateDTO = "Ya fue utilizada";
            }

            return StateDTO;
        }

        public IEnumerable<InvitationDTO> GetInvitations()
        {
            List<InvitationDTO> invitationDTOs = new List<InvitationDTO>();

            foreach (var invitation in _invitationRepository.GetInvitations())
            {
                invitationDTOs.Add(GetInvitationDTO(invitation));
            }

            return invitationDTOs;
        }

        public InvitationDTO UpdateInvitation(int id, InvitationIN invitationIn)
        {
            CheckExistsInvitationById(id);
            CheckInvitationIsActive(id);
            CheckCorrectRole(invitationIn.Role);

            Invitation invitation = _invitationRepository.GetInvitationById(id);
            invitation.UserName = invitationIn.UserName;
            invitation.Role = invitationIn.Role;
            invitation.Code = _guidService.RandomCode();

            CheckCorrectPharmacyAndRole(invitationIn.PharmacyName, invitationIn.Role);

            if (invitation.Role != 0)
            {
                invitation.Pharmacy = _pharmacyRepository.GetPharmacyByName(invitationIn.PharmacyName);
            }

            _invitationRepository.Update(invitation);

            return GetInvitationDTO(invitation);
        }

        private void CheckInvitationIsActive(int id)
        {
            if (!_invitationRepository.IsActive(id))
            {
                throw new InvalidOperationException("La invitación ya fue utilizada");
            }
        }

        private void CheckExistsInvitationById(int id)
        {
            if (!_invitationRepository.ExistsById(id))
            {
                throw new InvalidOperationException("La invitación no existe");
            }
        }
    }
}