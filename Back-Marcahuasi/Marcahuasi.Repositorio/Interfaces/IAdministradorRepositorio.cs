using Marcahuasi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio.Interfaces
{
    public interface IAdministradorRepositorio
    {
        Task<VMCambiarAdministrador> VerCredencialesDelAdministrador();
        Task<bool> EnviarCodigoDeConfirmacionDelAdministrador();
        bool ValidarCodigoDeConfirmacionDelAdministrador(string codigoRecibido);
        Task<bool> CambiarContraceniaDelAdministrador(string contracenia);
        Task<bool> CambiarAdministrador(DTOAdministrador administrador);
        Task<bool> IngresarCredenciales(DTOAdministradorLogeo administrador);
    }
}
