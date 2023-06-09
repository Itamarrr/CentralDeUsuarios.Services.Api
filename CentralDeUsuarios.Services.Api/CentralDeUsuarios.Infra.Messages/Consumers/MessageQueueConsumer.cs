using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Settings;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Consumers
{
    //classe para consumir e processar as mensagens da fila pra isso tenho que herda a clesse BackgroundService
    public class MessageQueueConsumer: BackgroundService
    {
        private readonly MessageSettings? _messageSettings;// injetando de dependencia para se ler os dados para poder conectar na fila 
        private readonly IServiceProvider? _serviceProvider;// para trabalhar com o consumer
        private readonly EmailHelper? _emailHelper;// para disparar o email
        private readonly IConnection? _connection;// conectar na fila 
        private readonly IModel? _model;// para ler as menssagens na fula 

        //CONSTRUTOR
        //IOptionsSnapshot server para injetar um obj cuja as informações foram capturado no appjson
        public MessageQueueConsumer(IOptionsSnapshot<MessageSettings> messageSettings, IServiceProvider? serviceProvider, EmailHelper? emailHelper)
        {
            _messageSettings = messageSettings.Value;
            _serviceProvider = serviceProvider;
            _emailHelper = emailHelper;
            #region Conectar no servidor de Mensageria
            var connectionFactory = new ConnectionFactory
            {
                HostName = _messageSettings.Host,
                UserName = _messageSettings.Username, 
                Password = _messageSettings.Password,

            };
            _connection = connectionFactory.CreateConnection(); // criando a conexão 

            _model = _connection.CreateModel();
            #region Cuidado caso mude as caracteristicas aqui será tambem mudada automaticamente no RabbitMQ
            _model.QueueDeclare(
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null
               );
            #endregion

            #endregion
        }

        //Metodo para ler a fila do RabbitMQ
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
        

        //Metodo para escrever e enviar o email de confirmação de cadastro de usaurio
        private void EnviarMensagemDeConfirmacaoDeCadastro(UsuariosMessageVO usuariosMessageVO)
        {
            var mailTo = usuariosMessageVO.Email;
            var subject = $"Confimação de cadastro de usuario. ID: {usuariosMessageVO.Id} ";
            var body = $@"
                          Olá {usuariosMessageVO.Nome},
                          <br/>
                          <br/>
                          <strong>Parabens, sua conta de usuario foi criada com suacesso! <strong/>
                          <br/>
                          <br/>
                            ID: <strong>   {usuariosMessageVO.Id}   </strong>
                            Nome: <strong> {usuariosMessageVO.Nome} </strong>
                          <br/>
                           Att, <br/>
                            Grupo de Capoeira Berimba.

                        ";

            _emailHelper.Send(mailTo, subject, body);
        }
    }
}
