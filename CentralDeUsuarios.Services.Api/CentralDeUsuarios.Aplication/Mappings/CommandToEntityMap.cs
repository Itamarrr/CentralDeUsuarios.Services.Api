using AutoMapper;
using CentralDeUsuario.Domain.Entities;
using CentralDeUsuarios.Aplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Aplication.Mappings
{
    //esse nome é porque eu pego os objeto do comand para o objeto map
    //ele Classe de Mapeamento do AutoMapper ele se[mpre herda o Projile
    public class CommandToEntityMap: Profile
    {
        public CommandToEntityMap() {
            CreateMap<CriarUsuarioCommand, Usuario>()//passar desse CriarUsuarioCommand para esse Usuario
                                                     //Como tem no usuario Id e o campo DataHoraDeCriacao eu posso fazer assim
                .AfterMap((command,entity )=>
                {
                    entity.Id = Guid.NewGuid();// gerando o novo id
                    entity.DataHoraDeCriacao = DateTime.Now; // gerando a data e hora atual 
                });
            
            
        }
    }
}
