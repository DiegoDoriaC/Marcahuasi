using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.DTOs
{
    public class VMRegistrosCompletos
    {
        public int IdIngreso { get; set; }
        public string? NombreTurista { get; set; }
        public string? ApellidoTurista { get; set; }
        public string? NumeroDocumentoTurista { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Modificado { get; set; }
        public string? Bandera { get; set; }
        public string? CodigoISO { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Departamento { get; set; }
    }
}
