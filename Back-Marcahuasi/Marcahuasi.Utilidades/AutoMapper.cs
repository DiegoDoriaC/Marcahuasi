using AutoMapper;
using Marcahuasi.DTOs;
using Marcahuasi.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Utilidades
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            //Creacion de los ViewModels (VM)

            #region Nacionalidad
            CreateMap<Nacionalidad, VMNacionalidad>();
            #endregion Nacionalidad

            #region TarifaPago
            CreateMap<TarifaPago, VMTarifaPago>();
            #endregion TarifaPago

            #region CambiarAdministrador
            CreateMap<Administrador, VMCambiarAdministrador>();
            #endregion CambiarAdministrador

            #region RegistrosBasicos
            CreateMap<Ingreso, VMRegistrosBasicos>()
                .ForMember(destino => destino.NombreTurista, options => options.MapFrom(origen => origen.IdTuristaNavigation!.Nombres))
                .ForMember(destino => destino.ApellidoTurista, options => options.MapFrom(origen => origen.IdTuristaNavigation!.Apellidos))
                .ForMember(destino => destino.NumeroDocumentoTurista, options => options.MapFrom(origen => origen.IdTuristaNavigation!.NumeroDocumento));
            #endregion RegistrosBasicos

            #region RegistrosCompletos
            CreateMap<Ingreso, VMRegistrosCompletos>()
                .ForMember(destino => destino.NombreTurista, options => options.MapFrom(origen => origen.IdTuristaNavigation!.Nombres))
                .ForMember(destino => destino.ApellidoTurista, options => options.MapFrom(origen => origen.IdTuristaNavigation!.Apellidos))
                .ForMember(destino => destino.NumeroDocumentoTurista, options => options.MapFrom(origen => origen.IdTuristaNavigation!.NumeroDocumento))
                .ForMember(destino => destino.Bandera, options => options.MapFrom(origen => origen.IdTuristaNavigation!.IdNacionalidadNavigation!.UrlImagen))
                .ForMember(destino => destino.CodigoISO, options => options.MapFrom(origen => origen.IdTuristaNavigation!.IdNacionalidadNavigation!.CodigoIso))
                .ForMember(destino => destino.Nacionalidad, options => options.MapFrom(origen => origen.IdTuristaNavigation!.IdNacionalidadNavigation!.Pais))
                .ForMember(destino => destino.Departamento, options => options.MapFrom(origen => origen.IdTuristaNavigation!.IdDepartamentoNavigation!.NombreDepartamento));
            #endregion RegistrosCompletos



            //Creacion de los Data Transfer Objects (DTO)

            #region Administrador
            CreateMap<DTOAdministrador, Administrador>()
                .ForMember(destino => destino.Nombre, options => options.MapFrom(origen => origen.NombreAdministrador));
            #endregion Administrador
        }
    }
}
