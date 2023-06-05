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

        //HttpPost para criar um usuario
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
