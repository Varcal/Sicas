using Domain.Financeiro.Entities.RecibosSindicantes;
using SharedKernel.Helpers;

namespace Application.Financeiro.ViewModels.Lancamentos
{
    public class LancamentoListVm
    {
        public int Id { get; set; }
        public int ReciboId { get; set; }
        public int LancamentoTipoId { get; set; }
        public string LancamentoTipo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }
        public int TipoFinanceiroId { get; set; }
        public string TipoFinanceiro { get; set; }

        public LancamentoListVm()
        {

        }

        public LancamentoListVm(Lancamento lancamento)
        {
            Id = lancamento.Id;
            ReciboId = lancamento.ReciboId;
            LancamentoTipoId = (int)lancamento.LancamentoTipo;
            LancamentoTipo = lancamento.LancamentoTipo.GetDescription();
            Descricao = lancamento.Descricao;
            Valor = lancamento.Valor;
            Observacao = lancamento.Observacao;
            TipoFinanceiroId = (int)lancamento.TipoFinanceiro;
            TipoFinanceiro = lancamento.TipoFinanceiro.GetDescription();
        }
    }
}
