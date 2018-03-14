using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class SindicanteTipo: EntityId
    {
        public string Nome { get; private set; }

        protected SindicanteTipo()
        {
            
        }

        public SindicanteTipo(string nome)
        {
            Nome = nome;
        }
    }
}