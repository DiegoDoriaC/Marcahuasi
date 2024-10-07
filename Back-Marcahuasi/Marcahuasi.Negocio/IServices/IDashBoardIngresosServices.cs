using Marcahuasi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Negocio.IServices
{
    public interface IDashBoardIngresosServices
    {
        Task<VMDashBoardIngresos> MostrarGraficoDeIngresos();
    }
}
