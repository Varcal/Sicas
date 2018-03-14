using System.ComponentModel;

namespace Domain.Core.Entities.DadosCadastros
{
    public enum PessoaTipo
    {
        [Description("Pessoa Física")]
        PessoaFisica = 1,
        [Description("Pessoa Jurídica")]
        PessoaJuridica = 2
    }
}