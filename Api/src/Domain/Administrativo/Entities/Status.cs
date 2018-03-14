using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class Status: EntityId
    {
        public string Nome { get; private set; }

        protected Status()
        {
        }

        public Status(string nome)
        {
            Nome = nome;
        }
    }
}