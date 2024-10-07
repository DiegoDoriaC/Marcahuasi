using AutoMapper;
using Marcahuasi.DTOs;
using Marcahuasi.Modelos;
using Marcahuasi.Repositorio.Interfaces;
using Marcahuasi.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Repositorio
{
    public class AdministradorRepositorio : IAdministradorRepositorio
    {

        private readonly MarcahuasiContext _context;
        private readonly IMapper _mapper;

        string codigoDeVerificacion = "";

        public AdministradorRepositorio(MarcahuasiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CambiarAdministrador(DTOAdministrador administrador)
        {
            try
            {
                Administrador adminMappeado = _mapper.Map<Administrador>(administrador);
                adminMappeado.IdAdministrado = 1;
                var entity = _context.Administrador.Update(adminMappeado);
                await _context.SaveChangesAsync();
                return entity.State == EntityState.Modified ? true : false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CambiarContraceniaDelAdministrador(string contracenia)
        {
            try
            {
                Administrador administrador = new()
                {
                    IdAdministrado = 1,
                    Contraceña = contracenia
                };
                var respuesta = _context.Administrador.Update(administrador);
                await _context.SaveChangesAsync();
                return respuesta.State == EntityState.Modified;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EnviarCodigoDeConfirmacionDelAdministrador()
        {
            try
            {
                Administrador? administradorActual = await _context.Administrador.FirstOrDefaultAsync();
                codigoDeVerificacion = MensajeriaTwilio.GenerarCodigo();
                return await MensajeriaTwilio.EnviarCodigo(codigoDeVerificacion, administradorActual!.NumeroTelefono);
            }
            catch
            {
                throw;
            }
        }

        public bool ValidarCodigoDeConfirmacionDelAdministrador(string codigoRecibido)
        {
            try
            {
                return MensajeriaTwilio.ComprobarCodigo(codigoDeVerificacion, codigoRecibido);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> IngresarCredenciales(DTOAdministradorLogeo administrador)
        {
            try
            {
                string passwordEncriptada = EncriptacionDeContracenia.GetSHA256(administrador.Contraceña!);
                Administrador? inicioSesion = await _context.Administrador.Where(x => x.NumeroTelefono == administrador.NumeroTelefono && x.Contraceña == passwordEncriptada).FirstOrDefaultAsync();
                if(inicioSesion != null) return true;
                return false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<VMCambiarAdministrador> VerCredencialesDelAdministrador()
        {
            try
            {
                VMCambiarAdministrador administradorFormateado = new VMCambiarAdministrador();
                Administrador? administrador = await _context.Administrador.FirstOrDefaultAsync();
                administradorFormateado.NombreCompleto = administrador?.Nombre;
                if (administrador != null)
                {
                    string celular = administrador.NumeroTelefono.ToString().Substring(-3);
                    string celularFormateado = "******" + celular;
                    int numeroCaraacteresContracenia = administrador.Contraceña.Length;
                    string contraceniaFormateada = new ('*', numeroCaraacteresContracenia);
                    administradorFormateado.NumeroCelular = celularFormateado;
                    administradorFormateado.Contracenia = contraceniaFormateada;
                }
                return administradorFormateado;
            }
            catch
            {
                throw;
            }
        }
    }
}
