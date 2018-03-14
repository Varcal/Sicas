using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Entities;
using SharedKernel.Validations;

namespace Domain.Administrativo.Scopes
{
    public static class ServicoSeguradoraScopes
    {
        public static bool CriarTipoServicoSeValido(this ServicoSeguradora servicoSeguradora)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(servicoSeguradora.Nome, "Nome é obrigatório"),
                    AssertionConcern.AssertNameIsValid(servicoSeguradora.Nome, "Nome não pode conter números"),
                    AssertionConcern.AssertTrue(servicoSeguradora.EventoTipos.Any(), "Tipo de Evento é obrigatório")
                );
        }

        public static bool AlterarTipoServicoSeValido(this ServicoSeguradora servicoSeguradora, string nome, ICollection<EventoTipo> eventoTipos)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(nome, "Nome é obrigatório"),
                    AssertionConcern.AssertNameIsValid(nome, "Nome não pode conter números"),
                    AssertionConcern.AssertTrue(eventoTipos.Any(), "Tipo de Evento é obrigatório")
                );
        }
    }
}
