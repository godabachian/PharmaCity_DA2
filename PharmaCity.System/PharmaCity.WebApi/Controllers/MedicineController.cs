using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.Domain.DTO.IN;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api/medicines")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpPost]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult PostMedicine([FromBody] MedicineIN medicine, [FromHeader] string token)
        {
            return Ok(_medicineService.InsertMedicine(medicine,token));
        }

        [HttpGet]
        public IActionResult GetMedicines([FromQuery] string nameMedicine, [FromQuery] string namePharmacy)
        {
            return Ok(_medicineService.GetMedicines(nameMedicine, namePharmacy));
        }

        [HttpDelete]
        [Route("{code}")]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult DeleteMedicine([FromRoute] string code)
        {
            _medicineService.DeleteMedicine(code);
            return Ok("Se ha eliminado correctamente la medicina");
        }


        [HttpPost]
        [Route("buy")]
        public IActionResult BuyMedicines([FromBody] Purchase purchase)
        {
            return Ok(_medicineService.BuyMedicines(purchase));
        }
    }
}