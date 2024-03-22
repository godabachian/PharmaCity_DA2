using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;
using System;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/invitations")]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            this._invitationService = invitationService;
        }

        [HttpPost]
        [ProtectFilter(RoleType.Administrator)]
        public IActionResult PostInvitation([FromBody] InvitationIN invitation)
        {
            return Ok(_invitationService.InsertInvitation(invitation));
        }

        [HttpGet]
        [ProtectFilter(RoleType.Administrator)]
        public IActionResult GetInvitations()
        {
            return Ok(_invitationService.GetInvitations());
        }

        [HttpPut]
        [Route("{id}")]
        [ProtectFilter(RoleType.Administrator)]
        public IActionResult PutInvitation([FromRoute] int id, [FromBody] InvitationIN invitation)
        {
            return Ok(_invitationService.UpdateInvitation(id, invitation));
        }

    }
}
