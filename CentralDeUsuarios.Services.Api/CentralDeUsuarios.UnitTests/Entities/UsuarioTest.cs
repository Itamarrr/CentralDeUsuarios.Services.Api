using CentralDeUsuario.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeUsuarios.UnitTests.Entities
{
    public class UsuarioTest
    {
        //classe de teste para entidade usuario

        [Fact]
        public void ValidarIdTest()
        {
            var usaurio = new Usuario
            {
                Id = Guid.NewGuid(),
            };
            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usaurio.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Id é Obrigatorio"))
                .Should().NotBeNull();
        }

        [Fact]
        public void ValidarNomeTest()
        {
            var usaurio = new Usuario
            {
                Nome= string.Empty,
            };
            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usaurio.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Nome Invalido Tem que conter no minimo 6 e maxio 150 caracteres"))
                .Should().NotBeNull();
        }

        [Fact]
        public void ValidarEmailTest()
        {
            var usaurio = new Usuario
            {
                Email = string.Empty,
            };
            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usaurio.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Endereço de Email Invalido"))
                .Should().NotBeNull();
        }

        [Fact]
        public void ValidarSenhaTest()
        {
            var usuario = new Usuario();

            usuario.Senha = string.Empty;

            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usuario.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter 8 maximo 10 caracteres"))
                .Should().NotBeNull();

            //mocando usuario administrador somente 
            usuario.Senha = "adminadmin";

            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usuario.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter ao menos uma letra maiuscula"))
                .Should().NotBeNull();

            //mocando usuario administrador somente 
            usuario.Senha = "ADMINADMIN";

            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usuario.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter ao menos uma letra minuscula"))
                .Should().NotBeNull();

            usuario.Senha = "adminADMIN";

            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usuario.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter ao menos um numero"))
                .Should().NotBeNull();


            usuario.Senha = "Admin1234";

            //Should me retorna alguma exceção se me devolver null significa que não tem a mensagem
            usuario.Validate.Errors.FirstOrDefault(er => er.ErrorMessage.Contains("Senha deve ter ao menos um caracter especial entre esses(! ou ? ou * ou @ ou # ou % ou $ ou & ou .)"))
                .Should().NotBeNull();

        }

    }
}
