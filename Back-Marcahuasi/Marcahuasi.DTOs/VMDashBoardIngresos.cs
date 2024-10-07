using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class VMDashBoardIngresos
    {
        public int TuristasExtranjeros { get; set; }
        public int TuristasNacionales { get; set; }
        public decimal RecaudacionNacionales { get; set; }
        public decimal RecaudacionExtranjeros { get; set; }
        public decimal RecaudacionTotal { get; set; }
    }
}
