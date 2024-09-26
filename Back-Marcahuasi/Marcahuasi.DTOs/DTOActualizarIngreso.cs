using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class DTOActualizarIngreso
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? NumeroDocumento { get; set; }
        public int IdNacionalidad { get; set; }
        public int IdDepartamento { get; set; }
        public int IdTarifa { get; private set; }
        public string Observacion { get; set; } = null!;


        public DTOActualizarIngreso()
        {
            IdTarifa = CalcularTarifa();
        }

        private int CalcularTarifa()
        {
            return IdNacionalidad == 1019 ? 1000 : 1001;
        }
    }
}
