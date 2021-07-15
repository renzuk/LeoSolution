using LeoSolution.Base;
using LeoSolution.Model;
using LeoSolution.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.RegularExpressions;

namespace LeoSolution.Controllers
{
    public class MetodoController : Controller
    {
        [HttpPost]
        [Route("metodo1")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioRetorno>> RetornaToken([FromBody] Usuario model)
        {
            var usuario = BaseUsuario.Get(model.Nome, model.Senha);

            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = Token.GerarToken(usuario);

            usuario.Senha = "";

            return new UsuarioRetorno()
            {
                usuario = usuario,
                autenticado = true,
                token = token,
                dataExpiracao = DateTime.Now.AddMinutes(5)
            };
        }


        [HttpPost]
        [Route("metodo2")]
        [Authorize]
        public async Task<ActionResult<SenhaValidacaoRetorno>> ValidaSenha([FromBody] SenhaValidacao senha)
        {
            bool senhaValida = true;
            string senhaValidar = senha.valor;

            Regex letraMaiuscula = new Regex(@"[A-Z]+");
            Regex letraMinuscula = new Regex(@"[a-z]+");
            Regex tamanhoMinimo = new Regex(@".{15,}");
            Regex caracteresSpeciais = new Regex(@"[!@#_-]");
            Regex caracteresRepetidos = new Regex(@"(.)\1");

            if (!letraMaiuscula.IsMatch(senhaValidar))
            {
                senhaValida = false;
            }
            else if (!letraMinuscula.IsMatch(senhaValidar))
            {
                senhaValida = false;
            }
            else if (!tamanhoMinimo.IsMatch(senhaValidar))
            {
                senhaValida = false;
            }
            else if (!caracteresSpeciais.IsMatch(senhaValidar))
            {
                senhaValida = false;
            }
            else if (caracteresRepetidos.IsMatch(senhaValidar))
            {
                senhaValida = false;
            }

            return new SenhaValidacaoRetorno()
            {
                SenhaValida = senhaValida
            };
        }

        [HttpGet]
        [Route("metodo3")]
        [Authorize]
        public async Task<ActionResult<SenhaGeracao>> GerarSenhaValida()
        {
            string charsMin = "abcdefghijklmnopqrstuvwxyz";
            string charsMax = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string charsCaracters = "!@#_-";
            bool min = false;
            bool max = false;
            bool carac = false;
            string pass = "";
            Random random = new Random();
            string anterior = "";
            string atual = "";

            while (pass.Length < 15)
            {
               
                int tipo = random.Next(1, 4);

                switch (tipo)
                {
                    case 1:
                        atual = charsMin.Substring(random.Next(0, charsMin.Length - 1), 1);
                        min = true;
                        break;
                    case 2:
                        atual = charsMax.Substring(random.Next(0, charsMax.Length - 1), 1);
                        max = true;
                        break;
                    case 3:
                        atual = charsCaracters.Substring(random.Next(0, charsCaracters.Length - 1), 1);
                        carac = true;
                        break;
                    default:
                        break;
                }

                if (anterior != atual)
                {
                    anterior = atual;
                    pass = pass + atual;
                }

            }

            pass = !min ? pass + charsMin.Substring(random.Next(0, charsMin.Length - 1), 1) : pass;
            pass = !max ? pass + charsMax.Substring(random.Next(0, charsMax.Length - 1), 1) : pass;
            pass = !carac ? pass + charsCaracters.Substring(random.Next(0, charsCaracters.Length - 1), 1) : pass;

            return new SenhaGeracao()
            {
                Senha = pass,
            };
        }

    }
}
