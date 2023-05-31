using CentralDeUsuario.Domain.Entities;
using CentralDeUsuario.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Infra.Data.Contexts;
using CentralDeUsuarios.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Repositories
{
    //classe para inplementar o repositorio de usuario
    public class UsuarioRepository :BaseRepository<Usuario, Guid>, IUsuarioRepository       
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        //Construtor para injeção de dependencia
        public UsuarioRepository(SqlServerContext sqlServerContext)
            : base(sqlServerContext) //base -> faz referencia a superclasse pois ela tambem tem que tr o  estou falando que esse oque vai ser injetado no construtor eu tambem quero que seja tambem injetado na classe pai
        {
            _sqlServerContext = sqlServerContext;
        }

        public override void Create(Usuario entity)// AQUI ESTOU SOBRESCREVENDO O METODO CREAT DO BaseRepository somente 
                                                   // para acrescentar a cripitografia da senha depois de criptografar eu mando para gravar
        {
            entity.Senha = MD5Helper.Encrypt(entity.Senha);// criptografando a senha
            base.Create(entity);//  gravando
        }
        public override void Update(Usuario entity)// AQUI ESTOU SOBRESCREVENDO O METODO alterar DO BaseRepository
        {
            entity.Senha = MD5Helper.Encrypt(entity.Senha);// criptografando a senha
            base.Update(entity);
        }

        public Usuario GetByEmail(string email)
        {
            return _sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email));// traga o primeiro email que seja igual a esse emai
        }
        
    }
}
