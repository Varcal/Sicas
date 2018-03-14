using System.Collections.Generic;
using CrossCutting.Reports.Base;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace CrossCutting.Reports.ReportsGenerator.Interfaces
{
    public interface IReciboSindicanteGeradorRelatorio
    {
        ReportDto GerarRelatorio(ReciboSindicante reciboSindicante);
        IList<ReportDto> GerarRelatorioEmLote(IEnumerable<ReciboSindicante> recibosSindicantes);
    }
}
