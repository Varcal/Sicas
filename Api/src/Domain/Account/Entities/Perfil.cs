using System.Collections.Generic;
using Domain.Core.Entities;

namespace Domain.Account.Entities
{
    public class Perfil: EntityId
    {
        public string Nome { get; private set; }

        public ICollection<Usuario> Usuarios { get; private set; } 

        protected Perfil()
        {
            
        }

        public Perfil(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}