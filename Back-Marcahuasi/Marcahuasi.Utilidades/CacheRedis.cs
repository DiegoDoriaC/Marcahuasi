using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Utilidades
{
    public static class CacheRedis
    {
        public static bool GuardarCodigo(string valor)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
                IDatabase db = redis.GetDatabase();
                db.StringSet("codigoConfirmacion", valor);
                redis.Close();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public static string ObtenerCodigo()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();            
            return db.StringGet("codigoConfirmacion")!;
        }
    }
}
