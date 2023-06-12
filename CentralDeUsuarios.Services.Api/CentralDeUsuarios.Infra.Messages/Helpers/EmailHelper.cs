using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Messages.Helpers
{
    //classe para envio de emails
    public class EmailHelper
    {
        private readonly MailSettings _mailSettings;

        public EmailHelper(IOptions<MailSettings> mailSettings)//IOptions é um obj que vamos usar paraa injetar as configurações na helpers
        {
            _mailSettings = mailSettings.Value;
        }

        //metodo para envio de email
        // mailTo esse é pra quem vou mandar o email
        // o subject é assunto
        // e body é o corpo do email
        public void Send(string mailTo, string subject, string body)
        {
            #region Escrevendo o email
            _mailSettings.Smtp = "smtp-mail.outlook.com";
            _mailSettings.Email = "grupoberimba@outlook.com.br";
            _mailSettings.Password = "passwordberinba";
            _mailSettings.Port = 587;

            //        "Email": "grupoberimba@outlook.com", // endereço ou seja aconta que vai mandar email
            //"Password": "passwordberinba", // a senha da conta que vai fazer o envio 
            //"Smtp": "smtp-mail.outlook.com", // servidor smtp dessa conta 
            //"Port": 587 // a porta 
            //Eu mandarei o email da conta que eu criei que é _mailSettings para a conta do mailTo
            //ou seja do remetente _mailSettings.Email para o destinatario mailTo
            var mailMessage = new MailMessage(_mailSettings.Email, mailTo);
            
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;// para mandar o email em html
            #endregion

            #region Enviando o email
            var smtpClient = new SmtpClient(_mailSettings.Smtp, _mailSettings.Port.Value);
            smtpClient.EnableSsl = true; // para mandar email com conexão segura
            smtpClient.Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password);// autenticando na conta que vai mandar email
            smtpClient.Send(mailMessage);// pra enviar o email
            #endregion

        }

    }
}
