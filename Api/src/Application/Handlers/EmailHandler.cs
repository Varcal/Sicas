using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Configuration;
using SharedKernel.DomainEvents.Events.Emails;
using SharedKernel.DomainEvents.Handlers.Base;

namespace Application.Handlers
{
    public class EmailHandler : Handler<Email>
    {
        public override void Handle(Email args)
        {
            var homologacao = Convert.ToBoolean(WebConfigurationManager.AppSettings["homologacao"]);

            if (homologacao)
                args.Destinatario = WebConfigurationManager.AppSettings["emailTestes"];


            var mail = new MailMessage
            {
                From = new MailAddress(args.Emitente),
                To = { new MailAddress(args.Destinatario)},
                Subject = args.Assunto,
                Body = args.Corpo,
                Priority = MailPriority.High,
                IsBodyHtml = true,
                SubjectEncoding = Encoding.GetEncoding("ISO-8859-1"),
                BodyEncoding = Encoding.GetEncoding("ISO-8859-1"),
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess,
                ReplyToList = { new MailAddress(args.Emitente) }
            };

            if(args.Anexo != null)
            {
                mail.Attachments.Add(new Attachment(args.Anexo, "arquivos.rar", MediaTypeNames.Application.Octet));
            }

            Send(mail);
        }

        private void Send(MailMessage email)
        {

            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential("contato@varcalsys.com.br", "Cleber30");

                client.Host = "mail.varcalsys.com.br";
                client.Port = 587;
                client.EnableSsl = false;           
                client.Credentials = credentials;
                client.Send(email);
            }
        }

        public override void Dispose()
        {
           
        }
    }
}
