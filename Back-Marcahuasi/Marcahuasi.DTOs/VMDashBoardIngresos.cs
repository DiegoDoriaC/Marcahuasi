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
        public int RecaudacionNacionales { get; set; }
        public int RecaudacionExtranjeros { get; set; }
        public int RecaudacionTotal { get; set; }
    }
}
