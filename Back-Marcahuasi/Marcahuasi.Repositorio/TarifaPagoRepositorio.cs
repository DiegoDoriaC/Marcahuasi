using AutoMapper;
using Marcahuasi.DTOs;
using Marcahuasi.Modelos;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio
{
    public class TarifaPagoRepositorio : ITarifaPagoRepositorio
    {

        private readonly MarcahuasiContext _context;
        private readonly IMapper _mapper;

        public TarifaPagoRepositorio(MarcahuasiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VMTarifaPago>> ObtenerTarifasDePago()
        {
            try
            {
                List<TarifaPago> listadoDeTarifasDePago = await _context.TarifaPagos.ToListAsync();
                return _mapper.Map<List<VMTarifaPago>>(listadoDeTarifasDePago);
            }
            catch
            {
                throw;
            }
        }
    }
}
