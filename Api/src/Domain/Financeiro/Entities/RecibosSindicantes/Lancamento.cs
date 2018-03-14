using System.Runtime.InteropServices;
using Domain.Core.Entities;
using SharedKernel.Enums;

namespace Domain.Financeiro.Entities.RecibosSindicantes
{
    public class Lancamento: EntityId
    {
        public int ReciboId { get; set; }
        public LancamentoTipo LancamentoTipo { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public string Observacao { get; private set; }
        public TipoFinanceiro TipoFinanceiro { get; private set; }

       

        protected Lancamento()
        {
            
        }

        public Lancamento(LancamentoTipo lancamentoTipo, string descricao, decimal valor, TipoFinanceiro tipoFinanceiro, [Optional]string observacao)
        {
            ReciboId = ReciboId;
            LancamentoTipo = lancamentoTipo;
            Descricao = descricao;
            Valor = valor;
            TipoFinanceiro = tipoFinanceiro;
            Observacao = observacao;
        }

        public Lancamento(int reciboId, LancamentoTipo lancamentoTipo, string descricao, decimal valor, TipoFinanceiro tipoFinanceiro, [Optional]string observacao)
            :this(lancamentoTipo, descricao, valor, tipoFinanceiro, observacao)
        {
            ReciboId = reciboId;
        }
    }
}
