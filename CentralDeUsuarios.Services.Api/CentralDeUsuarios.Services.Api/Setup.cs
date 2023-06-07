using CentralDeUsuario.Domain.Interfaces.Repositories;
using CentralDeUsuario.Domain.Interfaces.Services;
using CentralDeUsuarios.Aplication.Interfaces;
using CentralDeUsuarios.Aplication.Services;
using CentralDeUsuarios.Infra.Data.Repositories;

namespace CentralDeUsuarios.Services.Api
{
    //Essa classe é para configura minha injeção de dependencia do AspNet para ser acionada assim que rodar
    public static class Setup
    {
        public static void AddRegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();// injetando a interface IUsuarioAppService na classe UsuarioAppService
            builder.Services.AddTransient<IUsuarioDomainServices, UsuarioDomainServices>();// injetando a interface IUsuarioDomainServices na classe UsuarioDomainServices
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();// injetando a interface IUsuarioRepository na classe IUsuarioRepository
        }
    }
}
