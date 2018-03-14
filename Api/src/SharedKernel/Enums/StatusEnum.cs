using System.ComponentModel;

namespace SharedKernel.Enums
{
    public enum StatusEnum
    {
        [Description("Em Análise")]
        EmAnalise = 1,
        [Description("Emitir NF")]
        AguardandoEmissaoNf = 2,
        [Description("Enviado Seguradora")]
        EnviadoSeguradora = 3,
        [Description("Finalizado")]
        Finalizado = 4,
        [Description("Análise Finalizada")]
        FinalizadaAnalise = 5,
        [Description("Cancelado")]
        Cancelado = 6,
        [Description("Enviado Sindicante")]
        EnviadoSindicanteExterno = 7,
        [Description("Enviado Financeiro")]
        EnviadoFinanceiro = 8

    }
}
