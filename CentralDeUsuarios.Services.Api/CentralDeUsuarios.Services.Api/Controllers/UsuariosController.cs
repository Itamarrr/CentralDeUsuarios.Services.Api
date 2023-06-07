using CentralDeUsuario.Domain.Entities;
using CentralDeUsuarios.Aplication.Commands;
using CentralDeUsuarios.Aplication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralDeUsuarios.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //readonly é usuario para  quando é construtor esta abaixo a primeira dependencia
        //pra isso tive que fazer referencia do CentralDeUsuarios.Services.Api ao projeto CentralDeUsuarios.Aplication 
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        //HttpPost para criar um usuario
        [HttpPost]
        public IActionResult Post(CriarUsuarioCommand command)//CriarUsuarioCommand command é para assinar
                                                              //a requisição mas tambem poderia criar um objeto
        {
            _usuarioAppService.CriarUsuario(command);
            return  StatusCode(201, new //201 ou 200 é quando queremos retornar sucesso
            {
               message = "Usuario Cadastrado com sucesso.",
               command
            }); 
            
        }
    }
}
