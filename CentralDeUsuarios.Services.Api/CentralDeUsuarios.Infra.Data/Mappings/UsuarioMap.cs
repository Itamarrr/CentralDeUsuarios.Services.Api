using CentralDeUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Mappings
{
    //ORM Significa mapeamento objeto relacional
    //Classe de mapeamento orm para a entidade Usuario
    //IEntityTypeConfiguration -> biblioteca do fluentApi
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //feito somente para o email o restante o proprio entity framework faz sozinho por defolt
            builder.HasIndex(u => u.Email).IsUnique();

        }
    }
}
