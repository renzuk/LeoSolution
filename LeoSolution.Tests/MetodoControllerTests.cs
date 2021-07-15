using LeoSolution.Controllers;
using LeoSolution.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace LeoSolution.Tests
{
    public class MetodoControllerTests
    {
        [Fact]
        public async Task RetornaToken_UsuarioEsenhaInvalido_retornaNaoEncontrado()
        {
            var controller = new MetodoController();

            var resultado = await controller.RetornaToken(new Usuario {Nome = "teste",Senha="erro"});

            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        [Fact]
        public async Task RetornaToken_SenhaInvalida_retornaNaoEncontrado()
        {
            var controller = new MetodoController();

            var resultado = await controller.RetornaToken(new Usuario { Nome = "renato", Senha = "erro" });

            Assert.IsType<NotFoundObjectResult>(resultado.Result);
        }

        [Fact]
        public async Task RetornaToken_UsuarioEsenhaValido_retornaOk()
        {
            var controller = new MetodoController();

            var resultado = await controller.RetornaToken(new Usuario { Nome = "renato", Senha = "re01" });

            Assert.IsType<UsuarioRetorno>(resultado.Value);
            Assert.NotNull(resultado.Value.token);
        }

        [Fact]
        public async Task ValidaSenha_SenhaMinusculo_retornaFalso()
        {
            var controller = new MetodoController();

            var resultado = await controller.ValidaSenha(new SenhaValidacao { valor = "dnqwexyzarwvxym!" });

            Assert.False(resultado.Value.SenhaValida);
        }

        [Fact]
        public async Task ValidaSenha_SenhaCaracterEspecial_retornaFalso()
        {
            var controller = new MetodoController();

            var resultado = await controller.ValidaSenha(new SenhaValidacao { valor = "dnqWExyzaRWvxyM2" });

            Assert.False(resultado.Value.SenhaValida);
        }

        [Fact]
        public async Task ValidaSenha_SenhaLetraRepetidaMinuscula_retornaFalso()
        {
            var controller = new MetodoController();

            var resultado = await controller.ValidaSenha(new SenhaValidacao { valor = "dnqWExyzaaRWvxyM!1" });

            Assert.False(resultado.Value.SenhaValida);
        }

        [Fact]
        public async Task ValidaSenha_SenhaLetraRepetidaCaseSensitive_retornaTrue()
        {
            var controller = new MetodoController();

            var resultado = await controller.ValidaSenha(new SenhaValidacao { valor = "dnqWExyzaARWvxyM!1" });

            Assert.True(resultado.Value.SenhaValida);
        }

        [Fact]
        public async Task GerarSenhaValida_ValidaSenha_retornaTrue()
        {
            var controller = new MetodoController();

            var resultadoGeracao = await controller.GerarSenhaValida();

            var resultado = await controller.ValidaSenha(new SenhaValidacao { valor = resultadoGeracao.Value.Senha });

            Assert.True(resultado.Value.SenhaValida);
        }


    }
}
