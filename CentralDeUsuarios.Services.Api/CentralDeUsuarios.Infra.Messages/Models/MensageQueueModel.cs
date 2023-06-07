using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Models
{
    //classe para modelar as mensagens que seram escrita na fila
    public class MensageQueueModel
    {
        public Guid? Id { get; set; } = Guid.NewGuid(); //indentificadr da mensagem na fila já atribui o valor defolt
        public string? Conteudo { get; set; }// conteudo da mensagem na fila
        public DateTime? DataHoraCriacao { get; set; } = DateTime.Now;  // data e hora de scrita dessa menssagem na fila
    }
}
