using Microsoft.EntityFrameworkCore;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace PharmaCity.DataAccess
{
    public class InvitationRepository : IInvitationRepository
    {
        private PharmaCityDbContext _dbContext;

        public InvitationRepository(PharmaCityDbContext context)
        {
            this._dbContext = context;
        }

        public IEnumerable<Invitation> GetInvitations()
        {
            return _dbContext.Invitations.Include(invitation => invitation.Pharmacy).ToList();
        }

        public void InsertInvitation(Invitation invitation)
        {
            _dbContext.Invitations.Add(invitation);
            _dbContext.SaveChanges();
        }

        public void DeleteInvitation(Invitation invitation)
        {
            _dbContext.Invitations.Remove(invitation);
            _dbContext.SaveChanges();
        }

        public bool ExistsByCodeAndUserName(string code, string userName)
        {
            return _dbContext.Invitations.Any(invitation => invitation.Code == code && invitation.UserName == userName && invitation.State == State.Active);
        }

        public Invitation GetInvitationByCode(string code)
        {
            return _dbContext.Invitations.Include(invitation => invitation.Pharmacy).FirstOrDefault(invitation => invitation.Code == code);
        }

        public bool Exists(string userName)
        {
            return _dbContext.Invitations.Any(invitation => invitation.UserName == userName);
        }

        public RoleType GetRoleByCode(string code)
        {
            return _dbContext.Invitations.FirstOrDefault(invitation => invitation.Code == code).Role;
        }

        public Pharmacy GetPharmacyByCode(string code)
        {
            return _dbContext.Invitations.FirstOrDefault(invitation => invitation.Code == code).Pharmacy;
        }

        public void Update(Invitation invitation)
        {
            _dbContext.Invitations.Update(invitation);
            _dbContext.SaveChanges();
        }

        public bool ExistsById(int id)
        {
            return _dbContext.Invitations.Any(invitation => invitation.Id == id);
        }

        public Invitation GetInvitationById(int id)
        {
            return _dbContext.Invitations.Include(invitation => invitation.Pharmacy).FirstOrDefault(invitation => invitation.Id == id);
        }

        public bool IsActive(int id)
        {
            return _dbContext.Invitations.Any(invitation => invitation.Id == id && invitation.State == State.Active);
        }
    }
}