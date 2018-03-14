using System.Linq;
using Domain.Administrativo.Entities;
using SharedKernel.Validations;

namespace Domain.Administrativo.Scopes
{
    public static class SindicanteScopes
    {
        public static bool CriarSindicanteSeEscopoValido(this Sindicante sindicante)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(sindicante.DadosPessoaFisica.Nome, "Nome é obrigatório"),
                    AssertionConcern.AssertCpfIsValid(sindicante.DadosPessoaFisica.Cpf,"Cpf é obrigatório"),
                    AssertionConcern.AssertTrue(sindicante.Enderecos.Any(), "Endereço é obrigatório"),
                    AssertionConcern.AssertNotEmpty(sindicante.Email, "E-mail é obrigatório"),
                    AssertionConcern.AssertEmailIsValid(sindicante.Email, "E-mail inválido"),
                    AssertionConcern.AssertEmailIsValid(sindicante.Telefone, "Telefone é obrigatório"),
                    AssertionConcern.AssertEmailIsValid(sindicante.Celular, "Celular é obrigatório"),
                    AssertionConcern.AssertTrue(sindicante.Honorarios.Any(), "Honorário é obrigatório")
                );
        }
    }
}
