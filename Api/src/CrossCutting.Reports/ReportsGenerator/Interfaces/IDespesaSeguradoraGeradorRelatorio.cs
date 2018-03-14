using CrossCutting.Reports.Base;
using Domain.Financeiro.Entities.DespesasSeguradora;

namespace CrossCutting.Reports.ReportsGenerator.Interfaces
{
    public interface IDespesaSeguradoraGeradorRelatorio
    {
        ReportDto GerarRelatorio(DespesaSeguradora despesasSeguradora);
    }
}
