using Bogus;
using CentralDeUsuario.Domain.Entities;
using CentralDeUsuario.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeUsuarios.UnitTests.Services
{
    public class UsuarioDomainServiceTeste
    {
        //atributo
        private readonly IUsuarioDomainServices _usuarioDomainService;

        public UsuarioDomainServiceTeste(IUsuarioDomainServices usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        [Fact]
        public void TesteCrearUsuario()
        {
            try
            {
                 var usuario = NewUsuario();
                _usuarioDomainService.CriarUsuario(usuario);
            }
            catch (Exception e)
            {
                //Fail estou gerando um resultado de falha
                Assert.Fail(e.Message);
            }
        }
        [Fact]
        public void TestEmailJaCadastrado()
        {
            var usuario = NewUsuario();
            _usuarioDomainService.CriarUsuario(usuario);
            //estou definido como criterio par o teste passar que a execução do metodo
            //devera retornar uma exeção do tipo DomainException
            Assert.Throws<DomainException>(() => _usuarioDomainService.CriarUsuario(usuario));
        }

        private Usuario NewUsuario()
        {
            var faker = new Faker("pt_BR");
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = $"@{faker.Internet.Password(10)}",
                DataHoraDeCriacao = DateTime.Now
            };
            return usuario;
        }
    }
}
