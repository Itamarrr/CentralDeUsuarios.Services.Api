using CentralDeUsuarios.Infra.Logs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Interfaces
{
    //Interface para operaç~çoes nop MongoDB para collection LogUsuarios
    public interface ILogUsuariosPersistence
    {
        void Create(LogUsuarioModel model);
        void Update(LogUsuarioModel model);
        void Delete(LogUsuarioModel model);
        List<LogUsuarioModel> GetAll(DateTime dataMin, DateTime dataMax);// consultar o log por data
        List<LogUsuarioModel> GetAll(Guid usuarioId);// consultar o log por usuario especifico

    }
}
