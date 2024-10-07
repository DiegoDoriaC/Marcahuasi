using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

//using Twilio;
//using Twilio.Rest.Api.V2010.Account;

namespace Marcahuasi.Utilidades
{
    public class MensajeriaTwilio
    {

        public static string GenerarCodigo()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] numeroAleatorio = new byte[6];
                rng.GetBytes(numeroAleatorio);

                int codigo = Math.Abs(BitConverter.ToInt32(numeroAleatorio, 0)) % 1000000;
                return codigo.ToString("D6");
            }
        }

        public static async Task<bool> EnviarCodigo(string codigo, string number)
        {
            try
            {
                string? sid = Environment.GetEnvironmentVariable("twilioSID");
                string? token = Environment.GetEnvironmentVariable("twilioTOKEN");

                TwilioClient.Init(sid, token);

                var opcionesDelMensaje = new CreateMessageOptions(
                new PhoneNumber(number));
                opcionesDelMensaje.MessagingServiceSid = Environment.GetEnvironmentVariable("twilioServiceSID");
                opcionesDelMensaje.Body = codigo;
                var message = await MessageResource.CreateAsync(opcionesDelMensaje);
                Console.WriteLine(message.Body);
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public static bool ComprobarCodigo(string codigoEnviado, string codigoRecibido)
        {
            return codigoEnviado == codigoRecibido;
        }

    }
}
 