using CentralDeUsuario.Domain.Validations;
using CentralDeUsuarios.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuario.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade de domínio
    /// </summary>
    public class Usuario: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; } 
        public string? Senha { get; set; }
       public DateTime DataHoraDeCriacao { get; set; }

        public FluentValidation.Results.ValidationResult Validate => new UsuarioValidation().Validate(this);
    }
}
