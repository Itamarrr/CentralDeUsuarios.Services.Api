using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Consumers
{
    //classe para consumir e processar as mensagens da fila pra isso tenho que herda a clesse BackgroundService
    public class MessageQueueConsumer: BackgroundService
    {
        private readonly MessageSettings? _messageSettings;// injetando de dependencia para se ler os dados para poder conectar na fila 
        private readonly IConnection? _connection;// conectar na fila 
        private readonly IModel? _model;// para ler as menssagens na fula 
        private readonly IServiceProvider? _serviceProvider;// para trabalhar com o consumer
        private readonly EmailHelper? _emailHelper;// para disparar o email
    }
}
