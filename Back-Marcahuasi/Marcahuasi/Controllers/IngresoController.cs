using Marcahuasi.DTOs;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marcahuasi.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class IngresoController : Controller
    {

        private readonly IIngresoRepositorio _repository;

        public IngresoController(IIngresoRepositorio repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("ListarUltimosRegistros")]
        public async  Task<IActionResult> UltimosRegistros()
        {
            List<VM10UltimosRegistros> listado = await _repository.Obtener10UltimosRegistros();
            return listado != null ? Ok(listado) : NoContent();
        }

        [HttpGet]
        [Route("ListarRegistrosBasicos/{dni}")]
        public async Task<IActionResult> RegistrosBasicos(string dni)
        {
            List<VMRegistrosBasicos> listado = await _repository.ObtenerRegistrosBasicos(dni);
            return listado.Any() ? Ok(listado) : NotFound(new { mensaje = "Registro no encontrado" });
        }

        [HttpGet]
        [Route("ListarRegistrosCompletos")]
        public async Task<IActionResult> RegistrosCompletos([FromBody] FiltrosDeBusqueda filtros)
        {
            List<VMRegistrosCompletos> listado = await _repository.ObtenerRegistrosCompletos(filtros);
            return listado != null ? Ok(listado) : NotFound(new { mensaje = "Registro no encontrado" });
        }

        [HttpPost]
        [Route("RegistrarIngreso")]
        public async Task<IActionResult> RegistroIngreso([FromBody] DTOIngreso ingreso)
        {
            return await _repository.RegistrarIngreso(ingreso) ? Ok() : BadRequest();
        }

        [HttpPut]
        [Route("ActualizarIngreso")]
        public async Task<IActionResult> ActualizarRegistro([FromBody] DTOActualizarIngreso ingreso)
        {
            return await _repository.ActualizarIngreso(ingreso) ? Ok() : NotFound(new { mensaje = "Recurso a actualizar no encontrado" });
        }


    }
}
