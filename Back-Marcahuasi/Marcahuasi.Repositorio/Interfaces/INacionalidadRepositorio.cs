using Marcahuasi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio.Interfaces
{
    public interface INacionalidadRepositorio
    {
        Task<List<VMNacionalidad>> ObtenerNacionalidad();
    }
}
