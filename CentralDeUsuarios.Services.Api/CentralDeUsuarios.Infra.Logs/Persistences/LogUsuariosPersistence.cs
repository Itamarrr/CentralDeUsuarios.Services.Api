using CentralDeUsuarios.Infra.Logs.Contexts;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Logs.Persistences
{
    //Implementação da persistencia de dados para log de usuarios
    public class LogUsuariosPersistence : ILogUsuariosPersistence
    {
        private readonly MongoDBContext _mongoDBContext;

        public LogUsuariosPersistence(MongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public void Create(LogUsuarioModel model)
        {
            _mongoDBContext.LogUsuarios.InsertOne(model);
        }

        public void Update(LogUsuarioModel model)
        {
            var filterAlterar = Builders<LogUsuarioModel>.Filter.Eq(log => log.Id, model.Id);
            _mongoDBContext.LogUsuarios.ReplaceOne(filterAlterar, model);// nova forma de updete utilizando o ReplaceOne que vem do filtro acima 
        }

        public void Delete(LogUsuarioModel model)
        {
            var filterParaDeletar = Builders<LogUsuarioModel>.Filter.Eq(log => log.Id, model.Id);
            _mongoDBContext.LogUsuarios.DeleteOne(filterParaDeletar);
        }

        public List<LogUsuarioModel> GetAll(DateTime dataMin, DateTime dataMax)
        {
            var filterParaData = Builders<LogUsuarioModel>.Filter
                .Where(log => log.DataHora >= dataMin  && log.DataHora <= dataMax);
            return _mongoDBContext.LogUsuarios
                .Find(filterParaData)
                .SortByDescending(log => log.DataHora)// trazendo por ordem decrescente a data e hora
                .ToList();
        }

        public List<LogUsuarioModel> GetAll(Guid usuarioId)
        {
            var filterParaUsuario = Builders<LogUsuarioModel>.Filter
               .Eq(log => log.UsuarioId, usuarioId);
            
            return _mongoDBContext.LogUsuarios
                .Find(filterParaUsuario)
                .SortByDescending(log => log.DataHora)// trazendo o usuario por ordem decrescente a data e hora
                .ToList();
        }

       
    }
}
