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
        //Criando um metodo construtor ctor tab tab ele vai receber um cervico DbContextOptions<tipando ele por ele ser generio> dbContextOptions
        //(DbContextOptions<SqlServerContext> dbContextOptions) é uma dependencia por padrão
        //base(dbContextOptions) toda ves que usa base é porque estou acessando o contrutor da minha classe pai
        //esta pasando pata a classe pai nesse caso é a DbContext ou seja estou pegando esse argumento dbContextOptions
        //e passando para o construtor da DbContext ele vai receber todos os parametros para se conectar na base de dados
        //dentre eles a connectionString PARA CRIAR UM CONSTRUTOR É CTOR TAB TAB
        public SqlServerContext(DbContextOptions<SqlServerContext> dbContextOptions) 
            : base(dbContextOptions)        
        {
            //agora fazendo um injetor


        }
        
        //foi criado um arquivo appsetting.json para receber por injeção de dependencia
        //metodo para definir a string de conexão ou seja a conectionstring do projeto
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BD_CentralDeUsuarios;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}


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
