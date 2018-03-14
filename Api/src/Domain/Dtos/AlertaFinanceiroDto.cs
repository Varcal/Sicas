namespace Domain.Dtos
{
    public class AlertaFinanceiroDto
    {
        public int TotalEnviadoFinanceiro { get; set; }
        public int TotalAguardandoEmissaoNf { get; set; }
        public int TotalEnviadoParaSeguradora { get; set; }    
        public int TotalFinalizadosNoMes { get; private set; }

       
        public AlertaFinanceiroDto(int totalEnviadoFinanceiro, int totalAguardandoEmissaoNf, int totalEnviadoParaSeguradora, int totalFinalizadosNoMes)
        {
            TotalEnviadoFinanceiro = totalEnviadoFinanceiro;
            TotalAguardandoEmissaoNf = totalAguardandoEmissaoNf;
            TotalEnviadoParaSeguradora = totalEnviadoParaSeguradora;
            TotalFinalizadosNoMes = totalFinalizadosNoMes;
        }
    }
}
