using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class VMCambiarAdministrador
    {
        public string? NumeroCelular { get; set; }
        public string? Contracenia { get; set; }
        public string? NombreCompleto { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
