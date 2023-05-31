using CentralDeUsuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuario.Domain.Interfaces.Services
{
    //interface para dominio que seja relacionada ao usuario
    public interface IUsuarioDomainServices
    {
        //linguagem ubiqua é usar no dominio a mesma linguagem do negocio vai ser usada 
        //para criar um usuario eu receberei um usuario da entidade Usuario de dominio
        void CriarUsuario(Usuario usuario);//Metodo para criar um usuario a ideia é criar uma linguagem do mundo real
    }
}
