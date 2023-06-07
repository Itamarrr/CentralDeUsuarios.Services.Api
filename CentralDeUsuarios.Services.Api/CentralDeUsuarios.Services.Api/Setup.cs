using CentralDeUsuario.Domain.Interfaces.Repositories;
using CentralDeUsuario.Domain.Interfaces.Services;
using CentralDeUsuarios.Aplication.Interfaces;
using CentralDeUsuarios.Aplication.Services;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Repositories;
using CentralDeUsuarios.Infra.Messages.Helpers;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.EntityFrameworkCore;

namespace CentralDeUsuarios.Services.Api
{
    //Essa classe é para configura minha injeção de dependencia do AspNet para ser acionada assim que rodar
    public static class Setup
    {
        // injetando dependencias AddRegisterServices
        public static void AddRegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();// injetando a interface IUsuarioAppService na classe UsuarioAppService
            builder.Services.AddTransient<IUsuarioDomainServices, UsuarioDomainServices>();// injetando a interface IUsuarioDomainServices na classe UsuarioDomainServices
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();// injetando a interface IUsuarioRepository na classe IUsuarioRepository

        }

        // injetando dependencias AddEntityFramework
        public static void AddEntityFrameworkServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("CentralDeUsuarios");//  pegando a conection string
            builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));
        }

        // injetando dependencias AddMessageServices
        public static void AddMessageServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MessageSettings>(builder.Configuration.GetSection("MessageSettings"));//
            builder.Services.Configure<MessageSettings>(builder.Configuration.GetSection("MailSettings"));//

            builder.Services.AddTransient<MensageQueueProducer>();
            builder.Services.AddTransient<EmailHelper>();



        }

        // injetando dependencias AddMessageServices
        public static void AddAutoMapperServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }


    }
}
