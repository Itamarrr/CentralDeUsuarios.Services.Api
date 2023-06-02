using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Core
{
    //IDisposable estou falando que todo repositorio tem que implemtar o IDisposable que ele é um coletor de lixo
    /// <summary>
    /// Interface para abstração dos repositórios
    /// </summary>
    /// <typeparam name="TEntity">Define o tipo da entidade</typeparam>
    /// <typeparam name="TKey">Define o tipo do ID da entidade</typeparam>
    public interface IBaseRepository<TEntity, TKey>: IDisposable //IDisposable estou falando que todo repositorio tem que implemtar o
        //IDisposable que ele é um coletor de lixo vou implemtner na classe que herda
        where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> GetAll(); //Listar todos

        TEntity GetById(TKey id);
    }

}
