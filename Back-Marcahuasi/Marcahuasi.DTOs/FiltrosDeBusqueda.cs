using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class FiltrosDeBusqueda
    {
        public DateTime? Dia { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? TarifaProcedencia { get; set; }
    }
}
