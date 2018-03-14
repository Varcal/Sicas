using Domain.Administrativo.Entities;
using SharedKernel.Validations;

namespace Domain.Administrativo.Scopes
{
    public static class EventoScopes
    {
        public static bool CriarEventoScopeSeValido(this Evento evento)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertIsGreaterThan(evento.EventoTipoId, 0, "Tipo de Evento é obrigatório"),
                    AssertionConcern.AssertIsGreaterThan(evento.FranquiaKm, 0, "Franqui de Km é obrigatória"),
                    AssertionConcern.AssertIsGreaterThan(evento.Honorario, 0, "Valor do Evento é obrigatório")
                );
        }
    }
}
