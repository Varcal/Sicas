using SharedKernel.Enums;

namespace Application.Financeiro.ViewModels.Lancamentos
{
    public class LancamentoRegistrarVm
    {
        public int ReciboId { get; set; }
        public LancamentoTipo LancamentoTipo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }
        public TipoFinanceiro TipoFinanceiro { get; set; }

        

        public LancamentoRegistrarVm(int reciboId, int lancamentoTipo, string descricao, decimal valor, string observacao, int tipoFinanceiro)
        {
            ReciboId = reciboId;
            LancamentoTipo = (LancamentoTipo)lancamentoTipo;
            Descricao = descricao;
            Valor = valor;
            Observacao = observacao;
            TipoFinanceiro = (TipoFinanceiro)tipoFinanceiro;
        }
    }
}
