using Domain.Administrativo.Entities;
using SharedKernel.Validations;

namespace Domain.Administrativo.Scopes
{
    public static class SindicanteInternoScopes
    {
        public static bool CriarSeEscopoValido(SindicanteInterno sindicanteInterno)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotNull(sindicanteInterno.Usuario,"Usuário é obrigatório")
                );
        }
    }
}
