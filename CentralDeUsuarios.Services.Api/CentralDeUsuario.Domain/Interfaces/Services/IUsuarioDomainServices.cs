using CentralDeUsuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuario.Domain.Interfaces.Services
{
    //IDisposable para descartar o objeto ou seja limpar o lixo dos recurso que eu injeto
    //interface para dominio que seja relacionada ao usuario
    public interface IUsuarioDomainServices:IDisposable
    {
        //linguagem ubiqua é usar no dominio a mesma linguagem do negocio vai ser usada 
        //para criar um usuario eu receberei um usuario da entidade Usuario de dominio
        void CriarUsuario(Usuario usuario);//Metodo para criar um usuario a ideia é criar uma linguagem do mundo real
    }
}
