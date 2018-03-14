using System.ComponentModel;

namespace SharedKernel.Enums
{
    public enum DespesaTipoEnum
    {
        [Description("Hospedagem")]
        Hospedagem = 1,
        [Description("Refeição")]
        Refeicao = 2,
        [Description("Transporte")]
        Transporte = 3,
        [Description("Pedágio")]
        Pedagio = 4
    }
}
