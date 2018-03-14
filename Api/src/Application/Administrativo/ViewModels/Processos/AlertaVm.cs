using Domain.Dtos;

namespace Application.Administrativo.ViewModels.Processos
{
    public class AlertaVm
    {
        public int TotalAbertoMais5Dias { get; set; }
        public int TotalAberto3Dias { get; set; }
        public int TotalAguradandoParecer { get; set; }
        public int TotalEnviadoFinanceiro { get; set; }
        public int TotalAguardandoEmissaoNf { get; set; }
        public int TotalEnviadoParaSeguradora { get; set; }
        public int TotalFinalizadosNoMes { get; set; }

        protected AlertaVm()
        {

        }

        public AlertaVm(AlertaDto alerta)
        {
            TotalAbertoMais5Dias = alerta.TotalAbertoMais5Dias;
            TotalAberto3Dias = alerta.TotalAberto3Dias;
            TotalAguradandoParecer = alerta.TotalAguradandoParecer;
            TotalFinalizadosNoMes = alerta.TotalFinalizadosNoMes;
        }

        public AlertaVm(AlertaFinanceiroDto alerta)
        {
            TotalEnviadoFinanceiro = alerta.TotalEnviadoFinanceiro;
            TotalEnviadoParaSeguradora = alerta.TotalEnviadoParaSeguradora;
            TotalAguardandoEmissaoNf = alerta.TotalAguardandoEmissaoNf;
            TotalFinalizadosNoMes = alerta.TotalFinalizadosNoMes;
        }
    }
}
