using Domain.Financeiro.Entities.DespesasSeguradora;
using SharedKernel.Validations;

namespace Domain.Financeiro.Scopes
{
    public static class DespesaAdicionalScopes
    {
        public static bool CriarSeEscopoValido(this DespesaAdicional despesaAdicional)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertIsGreaterThan(despesaAdicional.DespesaTipoId, 0, "Tipo de despesa adicional é obrigatório"),
                    AssertionConcern.AssertTrue(despesaAdicional.Valor > 0, "Valor da despesa deve ser maior que 0")
                );
        }
    }
}
