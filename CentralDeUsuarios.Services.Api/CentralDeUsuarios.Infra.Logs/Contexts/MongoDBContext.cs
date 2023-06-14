using CentralDeUsuarios.Infra.Logs.Models;
using CentralDeUsuarios.Infra.Logs.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Contexts
{
    //Classe para conexão com o MongoDB
    public class MongoDBContext
    {
        private readonly MongoDBSettings _mongoDBSettings;
        private readonly IMongoDatabase _mongoDataBase;// para se conectar no mongo
        public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettings)//IOptions ajuda a trazer dados do meu appsettins.json
        {
            _mongoDBSettings = mongoDBSettings.Value;

            #region Conectando no banco de dados
            var client = MongoClientSettings.FromUrl(new (_mongoDBSettings.Host));
            if (_mongoDBSettings.IsSSL == "False")
            {             
                client.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                };

                _mongoDataBase = new MongoClient(client).GetDatabase(_mongoDBSettings.Nome);
            }
            #endregion
        }

        //propriedade para mapear a coleção do MongoDB OU SEJA NO MONGO TEM A ENTIDADE LogUsuarios Etem que se o nome certinho
        public IMongoCollection<LogUsuarioModel> LogUsuarios => _mongoDataBase.GetCollection<LogUsuarioModel>("LogUsuarios");
    }
}
