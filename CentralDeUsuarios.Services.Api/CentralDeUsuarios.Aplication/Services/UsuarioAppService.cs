using AutoMapper;
using CentralDeUsuario.Domain.Interfaces.Services;
using CentralDeUsuarios.Aplication.Commands;
using CentralDeUsuarios.Aplication.Interfaces;
using CentralDeUsuarios.Infra.Messages.Producers;
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
        private readonly IUsuarioDomainServices? _usuarioDomainServices; // dependencia do dominio que é o fluxo princiapal
        private readonly MensageQueueProducer? _mensageQueueProducer; //MensageQueueProducer é o que escreve na mensageria
        private readonly IMapper? _mapper; //AutoMaper para fazer o depara dos obj

        public UsuarioAppService(IUsuarioDomainServices usuarioDomainServices)
        {
            _usuarioDomainServices = usuarioDomainServices;
        }

        public void CriarUsuario(CriarUsuarioCommand command)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //Dispose para limpar sujeiras ou seja aquilo que injeta como dependencia depois joga fora
            _usuarioDomainServices.Dispose();
        }
    }
}
