using FluentValidation.Results;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Core
{
    /// <summary>
    /// Interface para definição de tipos de entidade
    /// </summary>
    /// <typeparam name="TKey">Tipo do ID da entidade</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Identificador da entidade
        /// </summary>
        /// 
        public TKey Id { get; set; }

        /// <summary>
        /// Retornar o resumo da validação da entidade
        /// </summary>
        /// 
        ValidationResult  Validate { get; }
    }
}
