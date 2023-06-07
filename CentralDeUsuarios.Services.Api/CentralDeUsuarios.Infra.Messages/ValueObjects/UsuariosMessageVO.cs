using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.ValueObjects
{
    //oBIJETO DE valor para gravar dados do usuario na mensagem da fila 
    public class UsuariosMessageVO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}
