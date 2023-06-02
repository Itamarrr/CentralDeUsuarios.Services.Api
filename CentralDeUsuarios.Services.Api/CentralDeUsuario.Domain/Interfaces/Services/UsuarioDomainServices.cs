using CentralDeUsuario.Domain.Entities;
using CentralDeUsuario.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuario.Domain.Interfaces.Services
{
    //A inplementação dos serviços de dominio de um usuario
    public class UsuarioDomainServices : IUsuarioDomainServices
    {
        //injeção de dependencia minha camada de dominio precisa do meu repositorio 
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioDomainServices(IUsuarioRepository usuarioRepository)// construtor criado em sima do private readonly IUsuarioRepository _usuarioRepository
                                                                          // ao selecionar clicar com direito do mouse; 
        {
            _usuarioRepository = usuarioRepository;
        }

        //Metodo para criar usuario na aplicação usaurio entidade de dominio
        public void CriarUsuario(Usuario usuario)
        {
            //a regra de negocio seria o seguinte se o e-mail existir na base de dados eu não cadastro
            DomainException.When(
                _usuarioRepository.GetByEmail(usuario.Email)!= null,
                $"O email {usuario.Email} já está cadastrado, tente outro."
                );//When -> Qundo usuariorepository for diferente de null

            _usuarioRepository.Create(usuario);

        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();//descartando ou limpando as sujeiras de onte estou injetando o _usuarioRepository
        }
    }
}
