using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Marcahuasi.Utilidades
{
    public static class EncriptacionDeContracenia
    {
            public static string GetSHA256(string str)
            {
            using (SHA256 sha256 = SHA256.Create()) // Usar SHA256.Create() en lugar de SHA256Managed
            {
                byte[] stream = sha256.ComputeHash(Encoding.ASCII.GetBytes(str)); // Encoding.ASCII en vez de ASCIIEncoding
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < stream.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", stream[i]);
                }
                return sb.ToString();
            }
        }
    }
}
