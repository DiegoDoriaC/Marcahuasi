using Marcahuasi.DTOs;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marcahuasi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TarifaPagoController : Controller
    {

        private readonly ITarifaPagoRepositorio? _repository;

        public TarifaPagoController(ITarifaPagoRepositorio? repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarTarifasDePago()
        {
            List<VMTarifaPago> tarifasDePago = await _repository!.ObtenerTarifasDePago();
            return tarifasDePago != null ? Ok(tarifasDePago) : NoContent();
        }
    }
}
