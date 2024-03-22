using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using System;
using PharmaCity.WebApi.Filters;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/login")]
    public class LoginController : ControllerBase
    {
        
        private readonly ISessionService _sessionService;

        public LoginController(ISessionService sessionService)
        {
            this._sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult LoginUser(Login login)
        {
            return Ok(_sessionService.Login(login));
        }
    }
}
