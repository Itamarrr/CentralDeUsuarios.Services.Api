using CentralDeUsuario.Domain.Entities;
using CentralDeUsuarios.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Contexts
{
    public class SqlServerContext: DbContext
    {

        //metodo para definir a string de conexão ou seja a conectionstring do projeto
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BD_CentralDeUsuarios;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        //metodo que podemos adcionar cada classe que foi feita no exemplo agora tem somente o UsuarioMap
        //conforme for criando mais classe  de mapeamento vamos passando elas abaixo 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            //modelBuilder.ApplyConfiguration(new UsuarioMap());
        }


        //propriedade para fornecer os metodos que serão utilizados no repoditorio de Usuarios
        //para cada entidade teremos um dbSet no instante temos somente usuario porem vai entrar mais 
        public DbSet<Usuario> Usuario { get; set; }
    }
}
