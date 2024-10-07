using Marcahuasi.DTOs;
using Marcahuasi.Negocio.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Marcahuasi.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class DashBoardIngresosController : Controller
    {

        private readonly IDashBoardIngresosServices _services;

        public DashBoardIngresosController(IDashBoardIngresosServices services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> MostrarGrafico()
        {
            
            VMDashBoardIngresos ingresos = await _services.MostrarGraficoDeIngresos();

            if (ingresos == null) return NotFound(new { mensaje = "Datos no encontrados" });

            return Ok(ingresos);
        }
    }
}
