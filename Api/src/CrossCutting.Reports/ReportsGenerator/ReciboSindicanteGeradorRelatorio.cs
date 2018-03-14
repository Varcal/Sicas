using System.Collections.Generic;
using System.Linq;
using CrossCutting.Reports.Base;
using CrossCutting.Reports.RecibosSindicantes;
using CrossCutting.Reports.ReportsGenerator.Base;
using CrossCutting.Reports.ReportsGenerator.Interfaces;
using Domain.Financeiro.Entities.RecibosSindicantes;
using Microsoft.Reporting.WebForms;

namespace CrossCutting.Reports.ReportsGenerator
{
    public class ReciboSindicanteGeradorRelatorio:GeradorRelatorio, IReciboSindicanteGeradorRelatorio
    {
        public ReportDto GerarRelatorio(ReciboSindicante reciboSindicante)
        {
            var reportDto = MontarRelatorio(reciboSindicante);

            return reportDto;
        }

        

        public IList<ReportDto> GerarRelatorioEmLote(IEnumerable<ReciboSindicante> recibosSindicantes)
        {
            var reports = new List<ReportDto>();

            foreach (var reciboSindicante in recibosSindicantes)
            {
                var report = MontarRelatorio(reciboSindicante);

                reports.Add(report);
            }


            return reports;
        }

        private ReportDto MontarRelatorio(ReciboSindicante reciboSindicante)
        {
            var reciboDto = new ReciboDto(reciboSindicante);
            var descontos = reciboDto.Lancamentos.Sum(x => x.Desconto);
            var outrosCreditos = reciboDto.Lancamentos.Sum(x => x.OutrosCreditos);

            var relatorio = new LocalReport()
            {
                ReportEmbeddedResource = "CrossCutting.Reports.RecibosSindicantes.ReciboSindicante.rdlc"
            };

            var ds = new ReportDataSource("ReciboSindicanteDs", reciboDto.Lancamentos);

            relatorio.SetParameters(new ReportParameter("NumeroRecibo", reciboDto.Numero));
            relatorio.SetParameters(new ReportParameter("Sindicante", reciboDto.Sindicante));
            relatorio.SetParameters(new ReportParameter("Cpf", reciboDto.Cpf));
            relatorio.SetParameters(new ReportParameter("DadosBancarios", reciboDto.DadosBancarios));
            relatorio.SetParameters(new ReportParameter("Descontos", descontos.ToString()));
            relatorio.SetParameters(new ReportParameter("OutrosCreditos", outrosCreditos.ToString()));
            relatorio.SetParameters(new ReportParameter("Total", reciboDto.Total.ToString()));

            relatorio.DataSources.Add(ds);

            deviceInfo = RetornarDeviceInfo();

            bytes = relatorio.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension,
                out streams, out warnings);

            var reportDto = new ReportDto(reciboDto.Sindicante, bytes, mimeType);

            return reportDto;
        }
    }
}
