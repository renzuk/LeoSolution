using LeoSolution.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LeoSolution.Service
{
    public class Token
    {
        public static string GerarToken(Usuario _usuario)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracao.ChaveJwt);
            var sectokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _usuario.Nome.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(sectokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
