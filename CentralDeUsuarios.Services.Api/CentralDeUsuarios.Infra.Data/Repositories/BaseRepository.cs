using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Repositories
{
    //é abstract pois não quero que essa classe não seja instanciada quero que seja somente  herdada estendida
    //classe generica para repositorio
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {

        //atributo 
        private readonly SqlServerContext _sqlServiceContext;// dependencia e criar meu metodo contrutor ctor tab tab

     
        protected BaseRepository(SqlServerContext sqlServiceContext)   //Metodo construtor gerado para injeção de dependencia
        {
            _sqlServiceContext = sqlServiceContext;
        }

        // ao selecionar tudo e clicar com mouse com botão direito ir em açoes rapidas

        //virtual toda vez que coloca estou falando que o metodo ele é subiscrito ou modificar na classe que esta sendo herdada
        //evitando alterar aqui na fonte
        public virtual void Create(TEntity entity)// O TEntity eu não sei quem é ainda ele vai ser subistituido pela herança
        {
            _sqlServiceContext.Entry(entity).State = EntityState.Added; // pega o obj prepara ele pra gravar no banco 
            _sqlServiceContext.SaveChanges();// executa a gravação
        }

        public virtual void Update(TEntity entity)
        {
            _sqlServiceContext.Entry(entity).State = EntityState.Modified; // pega o obj prepara ele pra modificar no banco 
            _sqlServiceContext.SaveChanges();// executa a modificação
        }

        public virtual void Delete(TEntity entity)
        {
            _sqlServiceContext.Entry(entity).State = EntityState.Deleted; // pega o obj prepara ele pra deletar no banco 
            _sqlServiceContext.SaveChanges();// executa a deleção
        }

        public virtual List<TEntity> GetAll()
        {
            return _sqlServiceContext.Set<TEntity>().ToList();// vai devolver uma lista de algum obj 
        }

        public virtual TEntity GetById(TKey id)
        {
            return _sqlServiceContext.Set<TEntity>().Find(id);//Find pegar a chave
        }

    }
}
