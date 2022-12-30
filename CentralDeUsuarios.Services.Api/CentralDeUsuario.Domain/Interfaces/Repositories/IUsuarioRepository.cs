using CentralDeUsuario.Domain.Entities;
using CentralDeUsuarios.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuario.Domain.Interfaces.Repositories
{
    //Interface de Repositorio para usuário
    public interface IUsuarioRepository: IBaseRepository<Usuario, Guid>
    {
        //metodo para consultar um usuario baseado no email
        Usuario GetByEmail(string email);
    }
}
