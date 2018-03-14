using System.ComponentModel;

namespace SharedKernel.Enums
{
    public enum TipoFinanceiro
    {
        [Description("Crédito")]
        Credito = 1,
        [Description("Débito")]
        Debito = 2
    }
}
