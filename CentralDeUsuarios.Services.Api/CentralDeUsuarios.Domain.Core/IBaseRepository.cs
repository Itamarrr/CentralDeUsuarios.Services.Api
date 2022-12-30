using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Core
{
    /// <summary>
    /// Interface para abstração dos repositórios
    /// </summary>
    /// <typeparam name="TEntity">Define o tipo da entidade</typeparam>
    /// <typeparam name="TKey">Define o tipo do ID da entidade</typeparam>
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> GetAll(); //Listar todos

        TEntity GetById(TKey id);
    }

}
