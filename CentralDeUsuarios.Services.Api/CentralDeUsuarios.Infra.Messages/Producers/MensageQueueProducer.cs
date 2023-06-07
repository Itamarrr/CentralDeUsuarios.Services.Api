using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Producers
{
    //Classe para escrita de mensagens na fila do RebbitMQ
    public class MensageQueueProducer
    {
        private readonly MessageSettings? _messsageSettings;// é aminha classe que vai trazer os parametros 
        private readonly ConnectionFactory? _connectionFactore;// é do proprio RebbitMQ

        //criando um construtor com IOptions é uma interface que me da um obj já preenchido apartir do appsettings.json
        public MensageQueueProducer(IOptions<MessageSettings> messageSettings)
        {
            _messsageSettings = messageSettings.Value;

            // faz a conexão com o servidor de mensageria (broker) estou pegando do appsettings e jogando para o _connectionFactore
            _connectionFactore = new ConnectionFactory
            {
                HostName = _messsageSettings.Host,
                UserName = _messsageSettings.Username, 
                Password = _messsageSettings.Password,

            };
        }

        // Metodo apara escrever a mensagem na fila
        public void Create(MensageQueueModel model)
        {
            //abrir a conexão do servidor de mensageria
            using (var connection =  _connectionFactore?.CreateConnection())
            {
                // criar um objeto na fila de mensagens
                using (var channel = connection?.CreateModel())
                {

                    // parametros para conexão da fila
                    channel?.QueueDeclare(
                        queue : _messsageSettings?.Queue,//  nome da fila
                        durable: true,//  não destruir ou apagar a fila
                        exclusive: false,//  só permite uma conexão por vez na fila
                        autoDelete: false,//  não exclui dados automaticamente, só se o cunsumer poder excluir
                        arguments: null //  sem argumentos
                        );

                    // escrevendo o conteudo da fila
                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _messsageSettings?.Queue,// nome da fila
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))// oque eu vou gravar na fila só que tenha que mandar em bytes tranformando ele em Json e mandando em Bytes
                        ); 

                }
            }

        }

    }
}
