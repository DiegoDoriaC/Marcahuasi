using Marcahuasi.DTOs;
using Marcahuasi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio.Interfaces
{
    public interface IIngresoRepositorio
    {
        //Metodos Getters
        Task<List<VM10UltimosRegistros>> Obtener10UltimosRegistros();
        Task<List<VMRegistrosBasicos>> ObtenerRegistrosBasicos(string DNI);
        Task<List<VMRegistrosCompletos>> ObtenerRegistrosCompletos(FiltrosDeBusqueda filtro);

        //Metodos Setters
        Task<bool> RegistrarIngreso(DTORegistroIngreso ingreso);
        Task<bool> ActualizarIngreso(DTOActualizarIngreso ingreso);

    }
}
