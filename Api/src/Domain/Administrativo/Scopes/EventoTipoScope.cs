using Domain.Administrativo.Entities;
using SharedKernel.Validations;

namespace Domain.Administrativo.Scopes
{
    public static class EventoTipoScope
    {
        public static bool CriarEventoTipoScopeSeValido(this EventoTipo eventoTipo)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(eventoTipo.Nome, "Nome é obrigatório"),
                    AssertionConcern.AssertNameIsValid(eventoTipo.Nome, "Nome não pode conter números")
                );
        }

    }
}
