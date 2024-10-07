using Marcahuasi.DTOs;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marcahuasi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AdministradorController : Controller
    {

        private readonly IAdministradorRepositorio _repository;

        public AdministradorController(IAdministradorRepositorio repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("VerCredenciales")]
        public async Task<IActionResult> CredencialesDelAdministrador()
        {
            VMCambiarAdministrador credenciales = await _repository.VerCredencialesDelAdministrador();
            return credenciales.NumeroCelular != null ? Ok(credenciales) : NotFound();
        }

        [HttpGet]
        [Route("EnvioCodigoConfirmacion")]
        public async Task<IActionResult> EnvioDelCodigoDeConfirmacion()
        {
            return await _repository.EnviarCodigoDeConfirmacionDelAdministrador() ? Ok() : StatusCode(500, new { menasje = "No se pudo enviar el codigo" });
        }

        [HttpPost]
        [Route("ValidarCodigoConfirmacion/{codigo}")]
        public IActionResult ValidarCodigoDeConfirmacion(string codigo)
        {
            return _repository.ValidarCodigoDeConfirmacionDelAdministrador(codigo) ? Ok() : BadRequest(new { mensaje = "El codigo no coincide" });
        }

        [HttpPut]
        [Route("ActualizarContracenia/{contracenia}")]
        public async Task<IActionResult> CambiarContracenia(string contracenia)
        {
            return await _repository.CambiarContraceniaDelAdministrador(contracenia) ? Ok() : StatusCode(500, new { mensaje = "No se pudo actualizar" });
        }

        [HttpPut]
        [Route("ActualizarAdministrador")]
        public async Task<IActionResult> ActualizarAdministrador([FromBody] DTOAdministrador administrador)
        {
            return await _repository.CambiarAdministrador(administrador) ? Ok() : StatusCode(500, new { menasje = "No se pudo actualizar" });
        }

        [HttpPost]
        [Route("IngresarCredenciales")]
        public async Task<IActionResult> Ingresar([FromBody] DTOAdministradorLogeo administrador)
        {
            return await _repository.IngresarCredenciales(administrador) ? Ok() : NotFound(new { mensaje = "Credenciales incorrectas" });
        }


    }
}
