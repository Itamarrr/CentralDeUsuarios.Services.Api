using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Settings
{
    //parametro de configuração para acesso ao MongoDB
    public class MongoDBSettings
    {
        public string? Host { get; set; }// Servidor do MongoDB
        public string? Nome { get; set; }// Nome do banco de dados
        public string? IsSSL { get; set; }// Security Socket Layer
    }
}
