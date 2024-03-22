using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.WebApi.Test
{
    [TestClass]
    public class InvitationControllerTest
    {

        private Mock<IInvitationService> _mock;
        private InvitationController _api;
        private InvitationIN _invitationIN;
        private Invitation _invitation;
        private InvitationDTO _invitationDto;
        private IEnumerable<InvitationDTO> _invitationsDTOs;
        private string _pharmacyName = "PharmaCity";

        [TestInitialize]
        public void Setup()
        {
            _mock = new Mock<IInvitationService>(MockBehavior.Strict);
            _api = new InvitationController(_mock.Object);

            _invitationIN = new InvitationIN
            {
                UserName = "PharmaCityUY",
                Role = RoleType.Administrator,
                PharmacyName = "PharmaCity"
            };

            _invitation = new Invitation
            {
                Id = 1,
                UserName = _invitationIN.UserName,
                Role = _invitationIN.Role,
                Pharmacy = new Pharmacy(),
                Code = "ABCD1234",
                State = State.Active
            };

            _invitationDto = new InvitationDTO
            {
                UserName = _invitationIN.UserName,
                Role = _invitationIN.Role.ToString()
            };

            _invitationsDTOs = new List<InvitationDTO>() { _invitationDto };
        }

        [TestMethod]
        public void PostInvitationTest()
        {
            _mock.Setup(x => x.InsertInvitation(_invitationIN)).Returns(_invitationDto);

            var result = _api.PostInvitation(_invitationIN);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as Invitation;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void GetInvitationsTest()
        {
            _mock.Setup(x => x.GetInvitations()).Returns(_invitationsDTOs);

            var result = _api.GetInvitations();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IList<Invitation>;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }

        [TestMethod]
        public void PutInvitationsTest()
        {
            _mock.Setup(x => x.UpdateInvitation(_invitation.Id, _invitationIN)).Returns(_invitationDto);

            var result = _api.PutInvitation(_invitation.Id, _invitationIN);
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;
            var body = objectResult.Value as IList<Invitation>;

            _mock.VerifyAll();
            Assert.AreEqual(200, statusCode);
        }
    }
}
