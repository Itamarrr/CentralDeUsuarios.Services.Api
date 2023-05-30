using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Contexts
{
    //class para injeção de dependencia executada toda vez que o Migrations for inicializado
    //ela herda a IDesignTimeDbContextFactory do tipo SqlServerContext
    //é possovel herdar e implementar a IDesignTimeDbContextFactory porque eu instalei no noguet o EntityFrameworkCore.Design
    public class SqlServerContexMigration : IDesignTimeDbContextFactory<SqlServerContext>
    {
        //injetor de dependencia para DbContaxt sempre que executarmos o recurso do Migrations do EntityFramework
        //esse metodo CreateDbContext vai injetar no meu SqlServerContext a dependencia que ele precisa que é uma herança do  DbContaxt .
        public SqlServerContext CreateDbContext(string[] args)
        {
            #region Localizar o arquivo appsettings.json

            
            //throw new NotImplementedException();
            //Para isso preciso capturar a conection scring dessa forma que é feita
            //tenho que localizar o arquivo appsettings.json
            var configurationBuilder = new ConfigurationBuilder();
            //estou falando para ele achar esse arquivo "appsettings.json" 
            var path = Path.Combine(Directory.GetCurrentDirectory(),"appsettings.json");
            //estou informando que o arquivo tem que existir
            configurationBuilder.AddJsonFile(path, false);

            //achou
            //agora pegar a string de conection dessa formar
            #endregion

            #region Capiturando o arquivo appsettings.json            
            //CentralDeUsuarios foi o nome dado para a connectionStrings dentro do arquivo appsettings.json"
            var root = configurationBuilder.Build();
            var connectionString = root.GetSection("ConnectionStrings").GetSection("CentralDeUsuarios").Value;
            #endregion

            #region Fazer a injeçao de dependência
            //agora farei a injeção de dependencia na classe SqlServerContext
            var builder = new DbContextOptionsBuilder<SqlServerContext>();
            builder.UseSqlServer(connectionString);
            return new SqlServerContext(builder.Options);
            #endregion

        }
    }
}
