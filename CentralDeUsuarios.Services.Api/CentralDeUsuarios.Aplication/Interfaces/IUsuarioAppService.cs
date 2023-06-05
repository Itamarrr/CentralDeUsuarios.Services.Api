using CentralDeUsuarios.Aplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Aplication.Interfaces
{
    //interface para abstração dos metodos da camada de aplicação paa usúario
    public interface IUsuarioAppService : IDisposable //IDisposable essa interface foi implementada para limpar sujeiras
    {
        //esse é o contrato de dados que temos que passar para esse metodo
        // isso que é intregado para API
        void CriarUsuario(CriarUsuarioCommand command);// Método para criar um usuario na aplicação ele recebe os dados atravez do commmand
    }
}
