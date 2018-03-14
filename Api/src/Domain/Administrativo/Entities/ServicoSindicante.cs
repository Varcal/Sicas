using System;
using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class ServicoSindicante : EntityId
    {
        public string Nome { get; private set; }


        protected ServicoSindicante()
        {
            
        }

        public ServicoSindicante(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}