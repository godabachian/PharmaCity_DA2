using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.Domain.DTO.IN;
using System.Collections.Generic;

namespace PharmaCity.IBusinessLogic
{
    public interface IInvitationService
    {
        InvitationDTO InsertInvitation(InvitationIN invitation);
        IEnumerable<InvitationDTO> GetInvitations();
        InvitationDTO UpdateInvitation(int id, InvitationIN invitation);
    }
}