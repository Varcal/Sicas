using System.Runtime.InteropServices;
using Domain.Financeiro.Entities.DespesasSeguradora;
using SharedKernel.Enums;

namespace Domain.Financeiro.Entities.RecibosSindicantes
{
    public class LancamentoDespesa: Lancamento
    {
        public int DespesaId { get; private set; }
        public Despesa Despesa { get; private set; }


        protected LancamentoDespesa()
        {
            
        }

        public LancamentoDespesa(int despesaId, LancamentoTipo lancamentoTipo, string descricao, decimal valor, TipoFinanceiro tipoFinanceiro, [Optional]string observacao)
            :base(lancamentoTipo, descricao, valor, tipoFinanceiro,  observacao)
        {
            DespesaId = despesaId;
        }
    }
}
