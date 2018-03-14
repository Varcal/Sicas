using Application.Financeiro.ViewModels;
using Application.Financeiro.ViewModels.Despesas;

namespace Application.Financeiro.Contracts
{
    public interface IDespesaAppService
    {
        void Registrar(ProcessoDespesaRegistrarVm model, string usuarioLogado);
        ProcessoDespesaVm ObterPorId(int id);
        void AlterarDespesa(DespesaVm despesa, string usuarioLogado);
        void Excluir(int id, string usuarioLogado);
        ReciboReportVm EmitirRecibo(int processoId, string usuarioLogado);
    }
}
