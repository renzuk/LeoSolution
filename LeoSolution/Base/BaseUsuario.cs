using LeoSolution.Model;
using System.Collections.Generic;
using System.Linq;

namespace LeoSolution.Base
{
    public class BaseUsuario
    {
        public static Usuario Get(string Nome, string Senha)
        {
            var Usuarios = new List<Usuario>();
            Usuarios.Add(new Usuario { Nome = "renato", Senha = "re01" });
            Usuarios.Add(new Usuario { Nome = "fabio", Senha = "fa02" });
            return Usuarios.Where(x => x.Nome.ToLower() == Nome.ToLower() && x.Senha == Senha).FirstOrDefault();
        }
    }
}
