using System;
using System.Collections.Generic;
using System.Linq;
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

        public EmailHelper(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        //metodo para envio de email
        // mailTo esse é pra quem vou mandar o email
        // o subject é assunto
        // e body é o corpo do email
        public void Send(string mailTo, string subject, string body)
        {
            #region Escrevendo o email
            //Eu mandarei o email da conta que eu criei que é _mailSettings para a conta do mailTo
            //ou seja do remetente _mailSettings.Email para o destinatario mailTo
            var mailMessage = new MailMessage(_mailSettings.Email, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;// para mandar o email em html
            #endregion

            #region Enviando o email
            var smtpClient = new SmtpClient(_mailSettings.Smtp, _mailSettings.Port.Value);
            smtpClient.Send(mailMessage);

            #endregion

        }

    }
}
