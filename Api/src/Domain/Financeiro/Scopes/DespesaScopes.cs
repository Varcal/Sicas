using System;
using Domain.Financeiro.Entities.DespesasSeguradora;
using SharedKernel.Validations;

namespace Domain.Financeiro.Scopes
{
    public static class DespesaScopes
    {
        public static bool CriarSeEscopoValido(this Despesa despesa)
        {
            return AssertionConcern.IsValid(
                AssertionConcern.AssertTrue(despesa.Data > DateTime.MinValue, "Data é obrigatória"),
                AssertionConcern.AssertIsGreaterThan(despesa.ProcessoId, 0, "Processo é obrigatório"),
                AssertionConcern.AssertNotEmpty(despesa.Descricao, "Descrição é obrigatoria"),
                AssertionConcern.AssertNotNull(despesa.Origem, "Endereço de partida é obrigatório"),
                AssertionConcern.AssertNotNull(despesa.Destino, "Endereço de chegada é obrigatório"),
                AssertionConcern.AssertTrue(despesa.Kms >= 0, "Kilometratgem não pode ser menor que 0")
                );
        }
    }
}
