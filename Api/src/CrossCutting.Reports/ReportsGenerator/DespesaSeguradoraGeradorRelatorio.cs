using CrossCutting.Reports.Base;
using CrossCutting.Reports.DespesasSeguradora;
using CrossCutting.Reports.ReportsGenerator.Base;
using CrossCutting.Reports.ReportsGenerator.Interfaces;
using Domain.Financeiro.Entities.DespesasSeguradora;
using Microsoft.Reporting.WebForms;

namespace CrossCutting.Reports.ReportsGenerator
{
    public class DespesaSeguradoraGeradorRelatorio: GeradorRelatorio, IDespesaSeguradoraGeradorRelatorio
    {
        public ReportDto GerarRelatorio(DespesaSeguradora despesaSeguradora)
        {
            var processoDto = new DespesaSeguradoraDto(despesaSeguradora);

            var relatorio = new LocalReport()
            {
                ReportEmbeddedResource = "CrossCutting.Reports.DespesasSeguradora.DespesaSeguradora.rdlc"
            };
            
            var ds = new ReportDataSource("DespesaSeguradoraDs", processoDto.Despesas);

            relatorio.SetParameters(new ReportParameter("NumeroRecibo", processoDto.NumeroRecibo));
            relatorio.SetParameters(new ReportParameter("Cia", processoDto.Cia));
            relatorio.SetParameters(new ReportParameter("Sinistro", processoDto.Sinistro));
            relatorio.SetParameters(new ReportParameter("Segurado", processoDto.Segurado));
            relatorio.SetParameters(new ReportParameter("Analista", processoDto.Analista));
            relatorio.SetParameters(new ReportParameter("DataConclusao", processoDto.DataConclusao.ToString("d")));
            relatorio.SetParameters(new ReportParameter("ValorKm", processoDto.ValorKm.ToString()));
            relatorio.SetParameters(new ReportParameter("ValorTotalPorExtenso", processoDto.ValorTotalPorExtenso));

            relatorio.DataSources.Add(ds);

            deviceInfo = RetornarDeviceInfo();

            bytes = relatorio.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension,
                out streams, out warnings);

            var reportDto = new ReportDto(processoDto.Cia, bytes, mimeType);

            return reportDto;
        }
    }
}
