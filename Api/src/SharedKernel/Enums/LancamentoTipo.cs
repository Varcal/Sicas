using System.ComponentModel;

namespace SharedKernel.Enums
{
    public enum LancamentoTipo
    {
        [Description("Despesas")]
        Despesa = 1,
        [Description("Honorário")]
        Honorario = 2,
        [Description("Vale")]
        Vale = 3,
        [Description("Outros")]
        Outros = 4
    }
}