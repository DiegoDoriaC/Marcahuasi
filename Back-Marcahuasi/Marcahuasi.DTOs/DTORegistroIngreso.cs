using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class DTORegistroIngreso
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? NumeroDocumento { get; set; }
        public int IdNacionalidad { get; set; }
        public int IdDepartamento { get; set; }
        public int IdTarifa { get; private set ; }


        public DTORegistroIngreso()
        {
            IdTarifa = CalcularTarifa();
        }

        private int CalcularTarifa()
        {
            return IdNacionalidad == 1019 ? 1000 : 1001;
        }
    }
}
