using Marcahuasi.DTOs;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marcahuasi.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class NacionalidadController : Controller
    {

        private readonly INacionalidadRepositorio _repository;

        public NacionalidadController(INacionalidadRepositorio repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarNacionalidad()
        {
            List<VMNacionalidad> nacionalidades = await _repository.ObtenerNacionalidad();

            if(nacionalidades == null ) return NotFound(new { mensaje = "Datos no encontrados" });

            return Ok(nacionalidades);
        }
    }
}
