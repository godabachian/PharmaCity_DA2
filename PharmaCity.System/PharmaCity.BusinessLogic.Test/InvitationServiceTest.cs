using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic.Test
{
    [TestClass]
    public class InvitationServiceTest
    {
        private Mock<IInvitationRepository> _mockInvitation;
        private IInvitationService invitationService;
        private InvitationIN _invitationIN;
        private Invitation _invitation;
        private InvitationIN _invitationAdminIN;
        private Invitation _invitationAdmin;
        private InvitationIN _invitationInvalidIN;
        private IEnumerable<Invitation> _invitations;
        private Mock<IPharmacyRepository> _mockPharmacy;
        private Mock<IGuidService> _mockGuid;
        private Pharmacy _pharmacy;

        [TestInitialize]
        public void Setup()
        {
            _mockInvitation = new Mock<IInvitationRepository>(MockBehavior.Strict);
            _mockPharmacy = new Mock<IPharmacyRepository>(MockBehavior.Strict);
            _mockGuid = new Mock<IGuidService>(MockBehavior.Strict);
            invitationService = new InvitationService(_mockInvitation.Object, _mockPharmacy.Object, _mockGuid.Object);

            _pharmacy = new Pharmacy
            {
                Name = "PharmaCity",
                Direction = "Av. 18 de Julio"
            };

            _invitationIN = new InvitationIN
            {
                UserName = "PharmaCityUY",
                PharmacyName = _pharmacy.Name,
                Role = RoleType.Employee

            };

            _invitation = new Invitation
            {
                Id = 0,
                UserName = _invitationIN.UserName,
                Pharmacy = _pharmacy,
                Role = _invitationIN.Role,
                Code = "ABCD1234",
                State = State.Active
            };

            _invitationAdminIN = new InvitationIN
            {
                UserName = "PharmaCityUY",
                Role = RoleType.Administrator,
                PharmacyName = ""
            };

            _invitationInvalidIN = new InvitationIN
            {
                UserName = "PharmaCityUY",
                PharmacyName = _pharmacy.Name,
                Role = RoleType.Other
            };

            _invitationAdmin = new Invitation
            {
                UserName = "PharmaCityUY",
                Role = RoleType.Administrator,
                Code = "ABCD1234",
                State = State.Inactive
            };

            _invitations = new List<Invitation>() { _invitation, _invitationAdmin };
        }

        [TestMethod]
        public void InsertInvitationTest()
        {
            _mockInvitation.Setup(x => x.Exists(_invitationIN.UserName)).Returns(false);
            _mockPharmacy.Setup(x => x.Exists(_invitationIN.PharmacyName)).Returns(true);
            _mockGuid.Setup(x => x.RandomCode()).Returns("ABCD1234");
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_invitationIN.PharmacyName)).Returns(_pharmacy);
            _mockInvitation.Setup(x => x.InsertInvitation(_invitation));
            invitationService.InsertInvitation(_invitationIN);
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        public void InsertAdminInvitationTest()
        {
            _mockInvitation.Setup(x => x.Exists(_invitationAdminIN.UserName)).Returns(false);
            _mockPharmacy.Setup(x => x.Exists(_invitationAdminIN.PharmacyName)).Returns(true);
            _mockGuid.Setup(x => x.RandomCode()).Returns("123456");
            _mockInvitation.Setup(x => x.InsertInvitation(_invitationAdmin));
            invitationService.InsertInvitation(_invitationAdminIN);
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La invitación ya existe")]
        public void InsertInvalidInvitationTest()
        {
            _mockInvitation.Setup(x => x.Exists(_invitation.UserName)).Returns(true);
            _mockPharmacy.Setup(x => x.Exists(_pharmacy.Name)).Returns(true);
            _mockGuid.Setup(x => x.NewGuid()).Returns("ABCD1234");
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _mockInvitation.Setup(x => x.InsertInvitation(_invitation));
            invitationService.InsertInvitation(_invitationIN);
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "No existe el rol ingresado")]
        public void InsertInvalidRoleInvitationTest()
        {
            _mockInvitation.Setup(x => x.Exists(_invitation.UserName)).Returns(false);
            _mockPharmacy.Setup(x => x.Exists(_pharmacy.Name)).Returns(true);
            _mockGuid.Setup(x => x.RandomCode()).Returns("123456");
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _mockInvitation.Setup(x => x.InsertInvitation(_invitation));
            invitationService.InsertInvitation(_invitationInvalidIN);
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        public void GetInvitationsTest()
        {
            _mockInvitation.Setup(x => x.GetInvitations()).Returns(_invitations);
            invitationService.GetInvitations();
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "La farmacia no existe")]
        public void InsertInvalidPharmacyInvitationTest()
        {
            _mockInvitation.Setup(x => x.Exists(_invitation.UserName)).Returns(false);
            _mockPharmacy.Setup(x => x.Exists(_pharmacy.Name)).Returns(false);
            
            invitationService.InsertInvitation(_invitationIN);

            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvitationTest()
        {
            _mockInvitation.Setup(x => x.ExistsById(_invitation.Id)).Returns(true);
            _mockInvitation.Setup(x => x.IsActive(_invitation.Id)).Returns(true);
            _mockInvitation.Setup(x => x.GetInvitationById(_invitation.Id)).Returns(_invitation);
            _mockGuid.Setup(x => x.RandomCode()).Returns("123456");
            _mockPharmacy.Setup(x => x.Exists(_pharmacy.Name)).Returns(true);
            _mockPharmacy.Setup(x => x.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _mockInvitation.Setup(x => x.Update(_invitation));

            invitationService.UpdateInvitation(_invitation.Id, _invitationIN);
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La invitación no existe")]
        public void UpdateInvalidInvitationTest()
        {
            _mockInvitation.Setup(x => x.ExistsById(_invitation.Id)).Returns(false);

            invitationService.UpdateInvitation(_invitation.Id, _invitationIN);
            _mockInvitation.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "La invitación ya fue utilizada")]
        public void UpdateInactiveInvitationTest()
        {
            _mockInvitation.Setup(x => x.ExistsById(_invitation.Id)).Returns(true);
            _mockInvitation.Setup(x => x.IsActive(_invitation.Id)).Returns(false);

            invitationService.UpdateInvitation(_invitation.Id, _invitationIN);
            _mockInvitation.VerifyAll();
        }

    }
}
