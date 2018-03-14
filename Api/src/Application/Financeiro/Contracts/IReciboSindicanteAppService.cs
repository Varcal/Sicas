using System;
using System.Collections.Generic;
using Application.Financeiro.ViewModels;
using Application.Financeiro.ViewModels.Lancamentos;

namespace Application.Financeiro.Contracts
{
    public interface IReciboSindicanteAppService
    {
        IEnumerable<ReciboSindicanteRelatorioVm> SelecionarPorSindicanteData(DateTime dataInicio, DateTime dataFim);
        ReciboReportVm EmitirRecibo(int id, string usuarioLogado);
        ReciboReportVm EmitirTodos(DateTime dataInicio, DateTime dataFim, string usuarioLogado);
    }
}
