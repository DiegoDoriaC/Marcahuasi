using Marcahuasi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio.Interfaces
{
    public interface ITarifaPagoRepositorio
    {
        Task<List<VMTarifaPago>> ObtenerTarifasDePago();
    }
}
