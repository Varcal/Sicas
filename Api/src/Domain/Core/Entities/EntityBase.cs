using System;

namespace Domain.Core.Entities
{
    public abstract class EntityBase
    {
        public DateTime DataCadastro { get; protected set; }
        public bool Ativo { get; protected set; }
        

        protected EntityBase()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
        }


        public virtual bool IsValid()
        {
            return false;
        }

        public virtual void Reativar()
        {
            Ativo = true;
        }

        public virtual void Desativar()
        {
            Ativo = false;
        }
    }
}
