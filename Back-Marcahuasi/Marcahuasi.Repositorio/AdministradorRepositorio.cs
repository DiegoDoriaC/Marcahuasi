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
                adminMappeado.Contraceña = EncriptacionDeContracenia.GetSHA256(administrador.Contraceña!);
                var entity = _context.Administrador.Update(adminMappeado);
                bool response = entity.State == EntityState.Modified ? true : false;
                await _context.SaveChangesAsync();
                return response;
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
                Administrador administrador = new() { IdAdministrado = 1 };
                _context.Administrador.Attach(administrador);
                administrador.Contraceña = EncriptacionDeContracenia.GetSHA256(contracenia);
                _context.Entry(administrador).Property(a => a.Contraceña).IsModified = true;
                await _context.SaveChangesAsync();
                return true;
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
                string codigoDeVerificacion = MensajeriaTwilio.GenerarCodigo();
                CacheRedis.GuardarCodigo(codigoDeVerificacion);
                bool response = await MensajeriaTwilio.EnviarCodigo(codigoDeVerificacion, administradorActual!.NumeroTelefono);
                return response;
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
                return MensajeriaTwilio.ComprobarCodigo(CacheRedis.ObtenerCodigo(), codigoRecibido);
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
                    string celular = administrador.NumeroTelefono.ToString().Substring(6);
                    string celularFormateado = "******" + celular;
                    int numeroCaraacteresContracenia = administrador.Contraceña.Length;
                    string contraceniaFormateada = "**************";
                    administradorFormateado.NumeroCelular = celularFormateado;
                    administradorFormateado.Contracenia = contraceniaFormateada;
                    administradorFormateado.FechaModificacion = administrador.FechaModificacion;
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
