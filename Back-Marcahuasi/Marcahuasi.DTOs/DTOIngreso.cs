using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class DTOIngreso
    {
        //Datos del Turista
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string NumeroDocumento { get; set; } = null!;
        public int? IdNacionalidad { get; set; }
        public int? IdDepartamento { get; set; }

        //Datos del ingreso
        public int? IdTarifa { get; set; }
        public DateTime? FechaIngreso { get; }
        public bool Modificado { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Observacion { get; set; }
        public string UserRegister { get; set; } = null!;
    }
}
