using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class Acionamento: EntityId
    {
        public string Nome { get; private set; }
        public byte[] Arquivo { get; private set; }

        protected Acionamento()
        {
            
        }

        public Acionamento(string nome, byte[] arquivo)
        {
            Nome = nome;
            Arquivo = arquivo;
        }

        public void Alterar(byte[] arquivo)
        {
            if(arquivo != null)
                Arquivo = arquivo;
        }
    }
}
