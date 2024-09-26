using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class VMNacionalidad
    {
        public int IdNacionalidad { get; set; }
        public string Pais { get; set; } = null!;
        public string CodigoIso { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
    }
}
