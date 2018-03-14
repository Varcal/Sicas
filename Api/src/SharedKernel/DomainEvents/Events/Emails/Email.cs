using System;
using System.IO;
using System.Runtime.InteropServices;
using SharedKernel.DomainEvents.Contracts;


namespace SharedKernel.DomainEvents.Events.Emails
{
    public class Email : IDomainEvent
    {
        public string Assunto { get; set; }
        public string Destinatario { get; set; }
        public string Emitente { get; set; }
        public string Corpo { get; set; }
        public MemoryStream Anexo { get; set; }
        public DateTime DataOccurred { get; }


        public Email(string assunto, string destinatario, string emitente, string corpo, [Optional]byte[] anexo)
        {
            Assunto = assunto;
            Destinatario = destinatario;
            Emitente = emitente;
            Corpo = RetornarCorpoComSaudacao(corpo);
            Anexo = anexo != null ? new MemoryStream(anexo): null;
            DataOccurred = DateTime.Now;
        }

        private string RetornarCorpoComSaudacao(string corpoEmail)
        {
            var dateTime = DateTime.Now;
            string saudacao;

            if (dateTime.Hour < 12)
            {
                saudacao = "Bom dia";
            }else if (dateTime.Hour > 12 && dateTime.Hour <= 18)
            {
                saudacao = "Boa tarde";
            }
            else
            {
                saudacao = "Boa noite";
            }

            return corpoEmail.Replace("%SAUDACAO%", saudacao);
        }
    }
}
