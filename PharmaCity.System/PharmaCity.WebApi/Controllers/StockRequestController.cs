using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;
using System;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/stockrequest")]
    public class StockRequestController : ControllerBase
    {
        private readonly IStockRequestService _stockRequestService;

        public StockRequestController(IStockRequestService stockRequestService)
        {
            this._stockRequestService = stockRequestService;
        }

        [HttpPost]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult PostRequest([FromBody] StockRequest stockRequest, [FromHeader] string token)
        {
            return Ok(_stockRequestService.InsertRequest(stockRequest, token));
        }


        [HttpGet]
        [ProtectFilter(RoleType.Owner)]
        public IActionResult GetRequests([FromHeader] string token)
        {
            return Ok(_stockRequestService.GetRequests(token));
        }

        [HttpPatch]
        [Route("accept/{id}")]
        [ProtectFilter(RoleType.Owner)]
        public IActionResult PatchAcceptRequest([FromRoute] int id, [FromHeader] string token)
        {
            _stockRequestService.AcceptRequest(id, token);
            return Ok("Se aceptó exitosamente la reposición de stock.");
        }

        [HttpPatch]
        [Route("decline/{id}")]
        [ProtectFilter(RoleType.Owner)]
        public IActionResult PatchDeclineRequest([FromRoute] int id, [FromHeader] string token)
        {
            _stockRequestService.DeclineRequest(id, token);
            return Ok("Se denegó exitosamente la reposición de stock.");
        }
    }
}
