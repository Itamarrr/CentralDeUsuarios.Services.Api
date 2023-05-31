using CentralDeUsuario.Domain.Interfaces.Repositories;
using CentralDeUsuario.Domain.Interfaces.Services;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.UnitTests
{
    //CLASSE PARA CONFIGURAÇÃO DA INJEÇÃO DE DEPENDENCIA NO PROJETO XUnit
    public class Setup:Xunit.Di.Setup
    {
        //metodo para configuraar a injeção de dependeicia para que ela funcione no XUnit
        protected override void Configure()
        {
            #region Ativar a injeção de dependencia no XUnit
            ConfigureAppConfiguration((hostingContext, config)=>{
                bool reloadOnChange = hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange",true);
                if (hostingContext.HostingEnvironment.IsDevelopment()) {
                    config.AddUserSecrets<Setup>(true, reloadOnChange);
                } 
                
            });

            #endregion

            ConfigureServices((context, services) =>
            {
                #region Criando as dependencias do projeto

                #region Localizar o arquivo appsettings.json


                //throw new NotImplementedException();
                //Para isso preciso capturar a conection scring dessa forma que é feita
                //tenho que localizar o arquivo appsettings.json
                var configurationBuilder = new ConfigurationBuilder();
                //estou falando para ele achar esse arquivo "appsettings.json" 
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
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

                #endregion

                #region Fazer a injeçao de dependência do projeto teste
                // AQUI EU FALO XUnit proucura ai no seu projeto o appsenttings.json que ta dentro do seu projeto dentro dele traz a string de conexão
                //injetando a connection string na classe SqlServerContext
                services.AddDbContext<SqlServerContext>(option => option.UseSqlServer(connectionString));

                //injetando a classe UsuarioRepository na interface IUsuarioRepository
                services.AddTransient<IUsuarioRepository, UsuarioRepository>();//UsuarioRepository é a classe que estou injetando na minha interface

                #endregion

                #region Injetar a classe UsuarioDomainServices na interface IUsuarioRepository
                services.AddTransient<IUsuarioDomainServices, UsuarioDomainServices>();
                #endregion
            });
    
        }
    }
}
