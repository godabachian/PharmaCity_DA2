using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;
using System;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] User user, [FromQuery] string code)
        {
            return Ok(_userService.InsertUser(user, code));
        }
    }
}
