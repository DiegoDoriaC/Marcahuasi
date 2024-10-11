using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class DTOActualizarIngreso
    {
        public int IdIngreso { get; set; }
        public int IdTarifa { get; set; }
        public string Observacion { get; set; } = null!;
    }
}
