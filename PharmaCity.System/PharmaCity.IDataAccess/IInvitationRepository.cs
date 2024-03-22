using PharmaCity.Domain;
using System.Collections.Generic;

namespace PharmaCity.IDataAccess
{
    public interface IInvitationRepository
    {
        void InsertInvitation(Invitation invitation);
        IEnumerable<Invitation> GetInvitations();
        void DeleteInvitation(Invitation invitation);
        bool ExistsByCodeAndUserName(string code, string userName);
        Invitation GetInvitationByCode(string code);
        bool Exists(string userName);
        bool ExistsById(int id);
        Invitation GetInvitationById(int id);
        RoleType GetRoleByCode(string code);
        Pharmacy GetPharmacyByCode(string code);
        void Update(Invitation invitation);
        bool IsActive(int id);
    }
}