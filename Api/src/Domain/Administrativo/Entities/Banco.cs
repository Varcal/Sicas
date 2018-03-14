using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class Banco: EntityId
    {
        public string Numero { get; private set; }
        public string Nome { get; private set; }


        protected Banco()
        {
            
        }

        public Banco(string numero, string nome)
        {
            Numero = numero;
            Nome = nome;
        }
    }
}
