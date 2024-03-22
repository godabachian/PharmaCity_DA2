using Microsoft.AspNetCore.Mvc;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using PharmaCity.WebApi.Filters;

namespace PharmaCity.WebApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("/api")]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exporterService;

        public ExportController(IExportService exporterService)
        {
            this._exporterService = exporterService;
        }

        [HttpGet]
        [Route("export")]
        [ProtectFilter(RoleType.Employee)]
        public IActionResult ExportMedicines([FromQuery] string mechanismName)
        {
            _exporterService.Export(mechanismName);
            return Ok("Se ha exportado correctamente.");
        }

        [HttpGet]
        [Route("exporters")]
        public IActionResult GetExporters()
        {
            return Ok(_exporterService.GetExporters());
        }
    }
}
