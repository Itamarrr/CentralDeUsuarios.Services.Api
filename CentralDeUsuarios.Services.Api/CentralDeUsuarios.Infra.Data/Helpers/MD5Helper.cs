using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Helpers
{
    //classe para poder implementar cripitografia do tipo MD5 (HASH)
    public static class MD5Helper
    {
        public static string Encrypt(string value)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));// peguei o valor converti em bite e mandei incripitar
                #region hash Transformar em string novamente
                var result = string.Empty;
                foreach (var item in hash)
                {
                    result += item.ToString("X2");// X2 -> Vai virar uma string hexadecimal
                    
                }
                return result;
                #endregion
            }
        }
    }
}
