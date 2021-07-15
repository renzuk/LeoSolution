using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoSolution
{
    public class Configuracao
    {
        public static string ChaveJwt = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("leo010203abcd")).ToString();
    }
}
