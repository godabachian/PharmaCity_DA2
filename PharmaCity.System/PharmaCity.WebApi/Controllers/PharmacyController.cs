using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/pharmacy")]
    public class PharmacyController : ControllerBase
    {
        private IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            this._pharmacyService = pharmacyService;
        }

        [HttpPost]
        [ProtectFilter(RoleType.Administrator)]
        public IActionResult PostPharmacy([FromBody] Pharmacy pharmacy)
        {
            return Ok(_pharmacyService.InsertPharmacy(pharmacy));
        }
        [HttpGet]
        public IActionResult GetPharmacy()
        {
            return Ok(_pharmacyService.GetPharmacies());
        }
    }
}