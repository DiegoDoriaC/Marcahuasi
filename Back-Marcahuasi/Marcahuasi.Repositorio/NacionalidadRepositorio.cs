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
    public class NacionalidadRepositorio : INacionalidadRepositorio
    {

        private readonly MarcahuasiContext _context;
        private readonly IMapper _mapper;

        public NacionalidadRepositorio(MarcahuasiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VMNacionalidad>> ObtenerNacionalidad()
        {
            try
            {
                List<Nacionalidad> listaDeNacionalidades = await _context.Set<Nacionalidad>().ToListAsync();
                return _mapper.Map<List<VMNacionalidad>>(listaDeNacionalidades);
            }
            catch
            {
                throw;
            }
        }
    }
}
