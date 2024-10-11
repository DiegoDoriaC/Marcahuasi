using AutoMapper;
using Marcahuasi.DTOs;
using Marcahuasi.Modelos;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio
{
    public class IngresoRepositorio : IIngresoRepositorio
    {

        private readonly MarcahuasiContext _context;
        private readonly IMapper _mapper;

        public IngresoRepositorio(MarcahuasiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //Metodos Getters

        public async Task<List<VM10UltimosRegistros>> Obtener10UltimosRegistros()
        {
            try
            {
                var listadoDeRegistros = await _context.Ingresos.Include(x => x.IdTuristaNavigation).OrderByDescending(x => x.IdIngreso)
                    .Select(x => new {x.IdIngreso, x.IdTuristaNavigation!.Nombres, x.IdTuristaNavigation.Apellidos, x.IdTuristaNavigation.NumeroDocumento})
                    .Take(10).ToListAsync();         
                
                List<VM10UltimosRegistros> ultimosRegistros = new();
                foreach (var item in listadoDeRegistros)
                {
                    VM10UltimosRegistros objeto = new()
                    {
                        IdRegistro = item.IdIngreso,
                        NombreTurista = item.Nombres,
                        ApellidoTurista = item.Apellidos,
                        NumeroDocumentoTurista = item.NumeroDocumento
                    };
                    ultimosRegistros.Add(objeto); 
                }
                return ultimosRegistros;
            } 
            catch
            {
                throw;
            }
        }

        public async Task<List<VMRegistrosBasicos>> ObtenerRegistrosBasicos(string DNI)
        {
            try
            {
                List<Ingreso> registrosEncontrados = await _context.Ingresos.Include(x => x.IdTuristaNavigation)
                    .Where(x => x.IdTuristaNavigation!.NumeroDocumento == DNI).ToListAsync();
                return _mapper.Map<List<VMRegistrosBasicos>>(registrosEncontrados);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VMRegistrosCompletos>> ObtenerRegistrosCompletos(FiltrosDeBusqueda filtro)
        {
            try
            {
                IQueryable<Ingreso> query = _context.Ingresos.AsQueryable();

                if (filtro.FechaInicio.HasValue)  query = query.Where(x => x.FechaIngreso == filtro.FechaInicio.Value.Date);
                if (filtro.Mes.HasValue) query = query.Where(x => x.FechaIngreso!.Value.Month == filtro.Mes.Value);
                if (filtro.Anio.HasValue) query = query.Where(x => x.FechaIngreso!.Value.Year == filtro.Anio.Value);
                if (filtro.TarifaProcedencia.HasValue) query = query.Where(x => x.IdTarifaNavigation!.IdTarifa == filtro.TarifaProcedencia.Value);
                if (filtro.FechaFin.HasValue && filtro.FechaFin.HasValue) query = query.Where(x => x.FechaIngreso >= filtro.FechaInicio!.Value && x.FechaIngreso <= filtro.FechaFin.Value);

                List<Ingreso> listadoFiltrado = await query.Include(x => x.IdTarifaNavigation)
                    .Include(x => x.IdTuristaNavigation!).ThenInclude(x => x.IdNacionalidadNavigation)
                    .Include(x => x.IdTuristaNavigation!).ThenInclude(x => x.IdDepartamentoNavigation)
                    .ToListAsync();

                return _mapper.Map<List<VMRegistrosCompletos>>(listadoFiltrado);
            }
            catch
            {
                throw;
            }
        }


        //Metodos Setters

        public async Task<bool> ActualizarIngreso(DTOActualizarIngreso ingreso)
        {
            try
            {
                var parametroIdIngreso = new SqlParameter("@p_IdIngreso", SqlDbType.Int) { Value = ingreso.IdIngreso };
                var parametroIdTarifa = new SqlParameter("@p_IdTarifa", SqlDbType.Int) { Value = ingreso.IdTarifa };
                var parametroObservacion = new SqlParameter("@p_Observacion", SqlDbType.VarChar, 300) { Value = ingreso.Observacion };

                int filasAfectadas = await _context.Database.ExecuteSqlRawAsync("EXEC sp_actualizarIngreso @p_IdIngreso, @p_IdTarifa, @p_Observacion", parametroIdIngreso, parametroIdTarifa, parametroObservacion);
                return filasAfectadas != 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RegistrarIngreso(DTORegistroIngreso ingreso)
        {
            try
            {
                //Creacion de la DataTable
                DataTable dataTable = new();
                dataTable.Columns.Add("Nombres", typeof(string));
                dataTable.Columns.Add("Apellidos", typeof(string));
                dataTable.Columns.Add("NumeroDocumento", typeof(string));
                dataTable.Columns.Add("IdNacionalidad", typeof(int));
                dataTable.Columns.Add("IdDepartamento", typeof(int));
                //Asignacion de valores al DataTable
                dataTable.Rows.Add(
                   ingreso.Nombres,
                   ingreso.Apellidos,
                   ingreso.NumeroDocumento,
                   ingreso.IdNacionalidad != 0 ? ingreso.IdNacionalidad : (object)DBNull.Value,
                   ingreso.IdDepartamento != 0 ? ingreso.IdDepartamento : (object)DBNull.Value
                );
                // Define los parámetros para tu procedimiento almacenado
                var idTarifaParam = new SqlParameter("@p_IdTarifa", SqlDbType.Int) { Value = ingreso.IdTarifa };
                var tablaTuristaParam = new SqlParameter("@p_tablaTurista", SqlDbType.Structured) { TypeName = "TypeTurista", Value = dataTable }; // 'dataTable' debe ser un DataTable que represente la tabla de turistas
                var filasAfectadas = await _context.Database.ExecuteSqlRawAsync("EXEC sp_registrarIngreso @p_tablaTurista, @p_IdTarifa", tablaTuristaParam, idTarifaParam);
                return filasAfectadas != 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
