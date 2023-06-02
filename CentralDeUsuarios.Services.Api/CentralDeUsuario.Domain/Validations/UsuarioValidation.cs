using CentralDeUsuario.Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuario.Domain.Validations
{
    /// <summary>
    /// Classe para validação da entidade Usuário
    /// </summary>
    public class UsuarioValidation: AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("Id é Obrigatorio");
            RuleFor(u => u.Nome).NotEmpty().Length(6, 150).WithMessage("Nome Invalido Tem que conter no minimo 6 e maxio 150 caracteres");
            RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage("Endereço de Email Invalido");
            RuleFor(u => u.Senha).NotEmpty().Length(8,10).Matches(@"[A-Z]+").WithMessage("Senha deve ter 8 maximo 10 caracteres");
            RuleFor(u => u.Senha).NotEmpty().Length(8,10).Matches(@"[A-Z]+").WithMessage("Senha deve ter ao menos uma letra maiuscula");
            RuleFor(u => u.Senha).NotEmpty().Length(8,10).Matches(@"[a-z]+").WithMessage("Senha deve ter ao menos uma letra minuscula");
            RuleFor(u => u.Senha).NotEmpty().Length(8,10).Matches(@"[0-9]+").WithMessage("Senha deve ter ao menos um numero");
            //A senha tem que ter almenos uma desse caracteres especiais abixo (! ou ? ou * ou @ ou # ou % ou $ ou & ou .)
            RuleFor(u => u.Senha).NotEmpty().Length(8,10).Matches(@"[!\?\*\@\#\%\$\&\.]+").WithMessage("Senha deve ter ao menos um caracter especial entre esses(! ou ? ou * ou @ ou # ou % ou $ ou & ou .)");

                
        }
    }
}
