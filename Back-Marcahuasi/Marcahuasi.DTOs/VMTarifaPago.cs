using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class VMTarifaPago
    {
        public int IdTarifa { get; set; }
        public string NombreTarifa { get; set; } = null!;
        public decimal MontoTarifa { get; set; }
    }
}
