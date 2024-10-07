using Marcahuasi.DTOs;
using Marcahuasi.Modelos;
using Marcahuasi.Negocio.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Negocio
{
    public class DashBoardIngresosServices : IDashBoardIngresosServices
    {

        private readonly MarcahuasiContext _context;

        public DashBoardIngresosServices(MarcahuasiContext context)
        {
            _context = context;
        }

        public async Task<VMDashBoardIngresos> MostrarGraficoDeIngresos()
        {
            try
            {
                int cantidadDeTuristasNacionales = await _context.Ingresos.Where(x => x.IdTarifa == 1000).CountAsync();
                int cantidadDeTuristasExtranjeros = await _context.Ingresos.Where(x => x.IdTarifa == 1001).CountAsync();

                decimal recaudacionNacional = await _context.Ingresos.Include(x => x.IdTarifaNavigation).Where(x => x.IdTarifa == 1000).SumAsync(x => x.IdTarifaNavigation!.MontoTarifa);
                decimal recaudacionExtranjero = await _context.Ingresos.Include(x => x.IdTarifaNavigation).Where(x => x.IdTarifa == 1001).SumAsync(x => x.IdTarifaNavigation!.MontoTarifa);
                decimal recaudacionTotal = recaudacionNacional + recaudacionExtranjero;

                VMDashBoardIngresos dashBoardIngresos = new()
                {
                    TuristasExtranjeros = cantidadDeTuristasExtranjeros,
                    TuristasNacionales = cantidadDeTuristasNacionales,

                    RecaudacionNacionales = recaudacionNacional,
                    RecaudacionExtranjeros = recaudacionExtranjero,
                    RecaudacionTotal = recaudacionTotal
                };

                return dashBoardIngresos;

            }
            catch
            {
                throw;
            }
        }
    }
}
