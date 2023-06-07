using AutoMapper;
using CentralDeUsuario.Domain.Entities;
using CentralDeUsuario.Domain.Interfaces.Services;
using CentralDeUsuarios.Aplication.Commands;
using CentralDeUsuarios.Aplication.Interfaces;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Aplication.Services
{
    //implementação dos serviços de aplicação
    public class UsuarioAppService : IUsuarioAppService
    {
        //readonly é usuario para  quando é construtor esta abaixo a primeira dependencia
        //pra isso tive que fazer referencia ao projeto CentralDeUsuarioDomain 
        private readonly IUsuarioDomainServices _usuarioDomainService; // dependencia do dominio que é o fluxo princiapal
        private readonly MensageQueueProducer _mensageQueueProducer; //MensageQueueProducer é o que escreve na mensageria
        private readonly IMapper _mapper; //AutoMaper para fazer o depara dos obj


        // selecionei minhas dependencias toda e mandei adcionar parametros no construtor se o construtor já estiver eu ter que adcionar novos parametros
        public UsuarioAppService(IUsuarioDomainServices usuarioDomainService, MensageQueueProducer mensageQueueProducer, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mensageQueueProducer = mensageQueueProducer;
            _mapper = mapper;
        }

        public void CriarUsuario(CriarUsuarioCommand command)
        {
            #region Capiturando e validando o usuario
            //com o Mapper podemos fazer assim ou seja gerando um usuario a partir do command (automapper)
            var usuario = _mapper?.Map<Usuario>(command);// ele esta falando Mapper pega o command e dele me traz um usuario 

            //executar a validação do usuario
            var validate = usuario?.Validate;
            if (!validate.IsValid)
            {
                throw new ValidationException(validate.Errors);

            }
            else
            {
                #region Cadastrando o usuario
                //criar usuario
                _usuarioDomainService.CriarUsuario(usuario);
                #endregion


                #region Enviando uma mensagem para a fila
                // Criando o conteudo da mensagem que sera enviada para fila
                var _messageQueueModel = new MensageQueueModel
                {   
                    Tipo = TipoMensagem.CONFIRMACAO_DE_CADASTRO,
                    Conteudo = JsonConvert.SerializeObject(usuario)
                };
                //enviar usuario para fila
                _mensageQueueProducer.Create(_messageQueueModel);
                #endregion

            }
            #endregion
            //Sem o AutoMaper eu teria que fazer o Maper assim ou seja eu teria que preencher na mão tudo
            //var usuario = new Usuario
            //{
            //    Id = Guid.NewGuid(),
            //    Nome = command.Nome,
            //    Email = command.Email,
            //    Senha = command.Senha;
            //    DataHoraDeCriacao = DateTime.Now,
            //};
        }

        public void Dispose()
        {
            //Dispose para limpar sujeiras ou seja aquilo que injeta como dependencia depois joga fora
            _usuarioDomainService.Dispose();
        }
    }
}
