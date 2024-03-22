using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.DataAccess.Test
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        private Invitation _invitation;
        private PharmaCityDbContext _context;
        private IInvitationRepository _invitationRepository;

        [TestInitialize]
        public void Setup()
        {
            _invitation = new Invitation
            {
                Id = 1,
                UserName = "PharmaCityUY",
                Role = RoleType.Administrator,
                Pharmacy = new Pharmacy(),
                Code = "ABCD1234",
                State = State.Active
            };

            _context = CreateContext();
            _invitationRepository = new InvitationRepository(_context);
        }

        [TestMethod]
        public void InsertInvitationTest()
        {
            _invitationRepository.InsertInvitation(_invitation);

            Invitation invitationInDataBase = _context.Invitations.Where<Invitation>(invitation => invitation.Id == invitation.Id).AsNoTracking().FirstOrDefault();

            Assert.AreEqual(_invitation.UserName, invitationInDataBase.UserName);
        }

        [TestMethod]
        public void GetAllInvitationTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            IEnumerable<Invitation> invitationRepository = _invitationRepository.GetInvitations();

            Assert.AreEqual(invitationRepository.Count(), 1);
        }

        [TestMethod]
        public void DeleteInvitationTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            _invitationRepository.DeleteInvitation(_invitation);

            Invitation invitationInDataBase = _context.Invitations.Where<Invitation>(invitation => invitation.Id == _invitation.Id).AsNoTracking().FirstOrDefault();

            Assert.IsNull(invitationInDataBase);
        }

        [TestMethod]
        public void ExistsInvitationByCodeAndUserNameTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.IsTrue(_invitationRepository.ExistsByCodeAndUserName(_invitation.Code, _invitation.UserName));
        }

        [TestMethod]
        public void GetInvitationByCodeTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.IsNotNull(_invitationRepository.GetInvitationByCode(_invitation.Code));
        }

        [TestMethod]
        public void GetInvitationByIdTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.IsNotNull(_invitationRepository.GetInvitationById(_invitation.Id));
        }

        [TestMethod]
        public void GetRoleByCodeTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.AreEqual(_invitationRepository.GetRoleByCode(_invitation.Code), _invitation.Role);
        }

        [TestMethod]
        public void GetPharmacyByCodeTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.AreEqual(_invitationRepository.GetPharmacyByCode(_invitation.Code), _invitation.Pharmacy);
        }

        [TestMethod]
        public void ExistsInvitationTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.IsTrue(_invitationRepository.Exists(_invitation.UserName));
        }

        [TestMethod]
        public void ExistsInvitationByIdTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.IsTrue(_invitationRepository.ExistsById(_invitation.Id));
        }

        [TestMethod]
        public void IsActiveTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            Assert.IsTrue(_invitationRepository.IsActive(_invitation.Id));
        }

        [TestMethod]
        public void UpdateInvitationTest()
        {
            _context.Invitations.Add(_invitation);
            _context.SaveChanges();

            _invitationRepository.Update(_invitation);

            Assert.IsTrue(_invitationRepository.Exists(_invitation.UserName));
        }

        private PharmaCityDbContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<PharmaCityDbContext>()
                .UseInMemoryDatabase("InvitationDb")
                .Options;

            var context = new PharmaCityDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
