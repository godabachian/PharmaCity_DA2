using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/shopping")]
    public class ShoppingController : ControllerBase
    {

        private IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            this._shoppingService = shoppingService;
        }

        [HttpGet]
        [Route("petitions")]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult GetPetitions([FromHeader] string token)
        {
            return Ok(_shoppingService.GetPetitions(token));
        }

        [HttpGet]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult GetShoppings()
        {
            return Ok(_shoppingService.GetShopping());
        }

        [HttpGet]
        [Route("{code}")]
        public IActionResult GetPurchaseState([FromRoute] string code)
        {
            return Ok(_shoppingService.GetPurchaseState(code));
        }

        [HttpPatch]
        [Route("accept/{id}")]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult PatchAcceptRequest([FromRoute] int id, [FromHeader] string token)
        {
            _shoppingService.AcceptRequest(id, token);
            return Ok("Se aceptó exitosamente la compra.");
        }

        [HttpPatch]
        [Route("decline/{id}")]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult PatchDeclineRequest([FromRoute] int id, [FromHeader] string token)
        {
            _shoppingService.DeclineRequest(id, token);
            return Ok("Se denegó exitosamente la compra.");
        }
    }
}
