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
        public Usuario? GetByEmail(string email)
        {
            return _sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email));// traga o primeiro email que seja igual a esse emai
        }
        //Comentato pois estou refatorando o codigo para herdar minha classe BaseRepository que já tem um crud
        //public void Create(Usuario entity)
        //{
        //    entity.Senha = MD5Helper.Encrypt(entity.Senha);
        //    _sqlServerContext.Entry(entity).State = EntityState.Added;
        //    //_sqlServerContext.Usuario.Add(entity); pode ser assim tambem
        //    _sqlServerContext.SaveChanges();
        //}

        //public void Update(Usuario entity)
        //{
        //    entity.Senha = MD5Helper.Encrypt(entity.Senha);
        //    _sqlServerContext.Entry(entity).State = EntityState.Modified;           
        //    _sqlServerContext.SaveChanges();
        //}

        //public void Delete(Usuario entity)
        //{
        //    _sqlServerContext.Entry(entity).State = EntityState.Deleted;
        //    //_sqlServerContext.Usuario.Remove(entity); pode ser assim tambem
        //    _sqlServerContext.SaveChanges();
        //}

        //public List<Usuario> GetAll()
        //{
        //    return _sqlServerContext.Usuario.ToList();
        //}


        //public Usuario GetById(Guid id)
        //{
        //    //Find sempre busca encima da chave primaria  id
        //    return _sqlServerContext.Usuario.Find(id);
        //}



        //public Usuario GetByEmail(string email)
        //{
        //    return _sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email));
        //}
    }
}
