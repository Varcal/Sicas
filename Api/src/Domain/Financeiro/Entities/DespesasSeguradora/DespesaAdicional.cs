using Domain.Core.Entities;

namespace Domain.Financeiro.Entities.DespesasSeguradora
{
    public class DespesaAdicional: EntityId
    {
        public int DespesaTipoId { get; private set; }
        public int DespesaId { get; private set; }
        public decimal Valor { get; private set; }


        protected DespesaAdicional()
        {
            
        }

        public DespesaAdicional(int despesaTipoId,  decimal valor)
        {
            DespesaTipoId = despesaTipoId;
            Valor = valor;
        }

        public DespesaAdicional(int id, int despesaId, int despesaTipoId, decimal valor)
            :this(despesaTipoId, valor)
        {
            Id = id;
            DespesaId = despesaId;
;        }
    }
}