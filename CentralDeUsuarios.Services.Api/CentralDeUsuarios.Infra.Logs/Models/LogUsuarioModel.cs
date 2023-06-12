using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Models
{
    //Modelo de dados que sera gravado no MongoDB
    public class LogUsuarioModel
    {
        public Guid Id { get; set; }
        public string? Operacao { get; set; }
        public string? Detalhes { get; set; }
        public DateTime DataHora { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
