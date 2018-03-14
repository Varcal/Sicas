using System;
using System.Collections.Generic;
using Application.Financeiro.ViewModels.Lancamentos;

namespace Application.Financeiro.Contracts
{
    public interface ILancamentoAppService
    {
        void Registrar(LancamentoRegistrarVm model, string usuarioLogado);
        ReciboVm ObterRecibo(int sindicanteId, int processoId);
        void Excluir(int id, string usuarioLogado);
        void FecharLancamento(int id, string usuarioLogado);
        void ReabrirLancamento(int body, string usuarioLogado);
        void FecharPagamento(List<int> recibosIds,DateTime dataInicio, DateTime dataFim, string usuarioLogado);
        IEnumerable<ReciboPagamentoListVm> SelecionarPorSindicanteData(int sindicanteId, DateTime dataInicio, DateTime dataFim);
    }
}
