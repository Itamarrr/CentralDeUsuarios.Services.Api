using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Settings;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
        public MessageQueueConsumer(IOptions<MessageSettings> messageSettings, IServiceProvider? serviceProvider, EmailHelper? emailHelper)
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
            // COMPONENTE PARA FAZER A LEITURA DA FILA EventingBasicConsumer classe que ler e processa minha fila
            var consumer = new EventingBasicConsumer(_model);
            //Fazer a leitura pra isso tenho que capiturar o sender e um  args
            consumer.Received += (sender, args) =>
            {
                 //ler um conteudo gravada na fla 
                 //Prara isso eu pego ela em bytes deposi converto para string
                 var contentArray = args.Body.ToArray(); // pegando em bytes 
                var contentString = Encoding.UTF8.GetString(contentArray);  // convertendo em string

                //Como vem em json tenho que deserializar pois vem em jsom
                var messageQueuModel = JsonConvert.DeserializeObject<MensageQueueModel>(contentString);

                //verificar o tipo de mensagem 
                switch (messageQueuModel.Tipo)
                {
                    case TipoMensagem.CONFIRMACAO_DE_CADASTRO:
                        //CreateScope ao ler a mensagem vai trar da fila
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            //capiturar os usuario contido na mensagem
                            var usuarioMenssageVO= JsonConvert.DeserializeObject<UsuariosMessageVO>(messageQueuModel.Conteudo);
                            //enviar email agora
                            EnviarMensagemDeConfirmacaoDeCadastro(usuarioMenssageVO);
                            //Comunicar ao Rabbit que a menssagem foi processada para retirar a mensagem da fila
                            _model.BasicAck(args.DeliveryTag, false);// eu falo mensagem foi processada pode retirar ela da fila
                        }

                        break;

                    case TipoMensagem.RECUPERACAO_DE_SENHA:
                        // todo aindda tenho que fazer
                        break;
                }


            };
            //executando o consumidor
            _model.BasicConsume(_messageSettings.Queue, false, consumer);
            return Task.CompletedTask;//CompletedTask informa qu a Received finalizou que não deu erro

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
