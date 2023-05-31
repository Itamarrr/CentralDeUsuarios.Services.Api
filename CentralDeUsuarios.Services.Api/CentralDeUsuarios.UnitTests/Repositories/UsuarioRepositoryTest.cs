using Bogus;
using Bogus.DataSets;
using CentralDeUsuario.Domain.Entities;
using CentralDeUsuario.Domain.Interfaces.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeUsuarios.UnitTests.Repositories
{
    //classe de teste para repositorio usuario
    public class UsuarioRepositoryTest
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        //construtor para injeção de dependencia gerado com selecionado com o mause o private readonly IUsuarioRepository _usuarioRepository;
        //clicado com o botão direito em ações rapidas e criar construtor
        public UsuarioRepositoryTest(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        //[Fact(Skip = "Não implementado")] em caso de não esta implementado coloco esse e tiro o [Fact]
        //[Fact] teste real
        //[Fact(Skip = "Não implementado")]
        [Fact]
        public  void TestCreate()
        {
            #region Realizando o cadastro de um usuario
            var usuario = CreateUsuario();
            #endregion

            #region Buscando o usuario por id para Verificar se o Usuario foi cadastrado
            var usuarioById = _usuarioRepository.GetById(usuario.Id);
            usuarioById.Should().NotBeNull(); // o usuario não pode vir null
            usuarioById.Nome.Should().Be(usuario.Nome);// o usuario tem que ser o mesmo nome que esta na base
            usuarioById.Email.Should().Be(usuario.Email);// o usuario tem que ser o mesmo email que esta na base
            #endregion
        }
        //[Fact(Skip = "Não implementado")]
        [Fact]
        public  void TestUpdate()
        {
            #region Realizando o cadastro de um usuario
            var usuario = CreateUsuario();
            #endregion

            #region Atualizar os dados do usuario que gravei
            var faker = new Faker("pt_BR");
            usuario.Nome = faker.Person.FirstName;
            usuario.Email = faker.Person.LastName;            
            _usuarioRepository.Update(usuario);

            #endregion
            #region Buscando o usuario nao banco
            var usuarioById = _usuarioRepository.GetById(usuario.Id);
            usuarioById.Should().NotBeNull(); // o usuario não pode vir null
            usuarioById.Nome.Should().Be(usuario.Nome);// o usuario tem que ser o mesmo nome que esta na base
            usuarioById.Email.Should().Be(usuario.Email);// o usuario tem que ser o mesmo email que esta na base
            #endregion
        }
        //[Fact(Skip = "Não implementado")]
        [Fact]
        public  void TestDelete()
        {
            #region Realizando o cadastro de um usuario
            var usuario = CreateUsuario();
            #endregion

            #region Deletar o usuario
            _usuarioRepository.Delete(usuario);
            #endregion

            #region Buscando o usuario nao banco para saber se ele não foi encontrado se deletei ele não tem que ser encontrado
            var usuarioById = _usuarioRepository.GetById(usuario.Id);
            usuarioById.Should().BeNull(); // o usuario tem que ser null            
            #endregion


        }
        //[Fact(Skip = "Não implementado")]
        [Fact]
        public  void TestGatAll()
        {
            #region Realizando o cadastro de um usuario
            var usuario = CreateUsuario();
            #endregion

            #region Consultar todos os usuarios que tem na base
            var lista = _usuarioRepository.GetAll();
            #endregion

            #region Verificar se dentro da lista esta o usaurio que eu cadastrei
            lista.FirstOrDefault(u => u.Id.Equals(usuario.Id)).Should().NotBeNull();// me de o primeiro usuario desta lista cuso o id seja  ao buscar na lista o meu usuario tem que esta na lista não pode ser null na lista
            #endregion
        }
        //[Fact(Skip = "Não implementado")]
        [Fact]
        public  void TestGatById()
        {
            #region Realizando o cadastro de um usuario
            var usuario = CreateUsuario();
            #endregion

            #region consultar o usuario atravez do id
            var usuarioById = _usuarioRepository.GetById(usuario.Id);
            #endregion

            #region Quero saber se o usuario foi retornado na consulta
            usuarioById.Should().NotBeNull(); // o usuario não pode vir null
            usuarioById.Nome.Should().Be(usuario.Nome);// o usuario tem que ser o mesmo nome que esta na base
            usuarioById.Email.Should().Be(usuario.Email);// o usuario tem que ser o mesmo email que esta na base
            #endregion
        }
        //[Fact(Skip = "Não implementado")]
        [Fact]
        public  void TestGatByEmail()
        {
            #region Realizando o cadastro de um usuario
            var usuario = CreateUsuario();
            #endregion

            #region consultar o usuario atravez por e-mail
            var usuarioEmail = _usuarioRepository.GetByEmail(usuario.Email);
            #endregion

            #region Quero saber se o usuario foi retornado na consulta
            usuarioEmail.Should().NotBeNull(); // o usuario não pode vir null
            usuarioEmail.Nome.Should().Be(usuario.Nome);// o usuario tem que ser o mesmo nome que esta na base
            usuarioEmail.Email.Should().Be(usuario.Email);// o usuario tem que ser o mesmo email que esta na base
            #endregion

            #region Quero saber se o usuario foi retornado na consulta
            usuarioEmail.Should().NotBeNull(); // o usuario não pode vir null
            usuarioEmail.Nome.Should().Be(usuario.Nome);// o usuario tem que ser o mesmo nome que esta na base
            usuarioEmail.Email.Should().Be(usuario.Email);// o usuario tem que ser o mesmo email que esta na base
            #endregion
        }

        //Metodo para criar um usaurio no repositorio
        private Usuario CreateUsuario()
        {
            #region Realizando o cadastro do usuario
            var faker = new Faker("pt_BR");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = faker.Person.FirstName,
                Email = faker.Person.LastName,
                Senha = $"@{faker.Internet.Password(10)}",
                DataHoraDeCriacao = DateTime.Now
            };
            _usuarioRepository.Create(usuario);
            return usuario;
            #endregion
        }
    }
}
