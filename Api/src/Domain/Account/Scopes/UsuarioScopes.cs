using System.Collections.Generic;
using Domain.Account.Entities;
using SharedKernel.Validations;

namespace Domain.Account.Scopes
{
    public static class UsuarioScopes
    {
        public static bool CriaUsuarioScopeSeValido(this Usuario usuario)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(usuario.Nome, "Nome é obrigatório"),
                    AssertionConcern.AssertNameIsValid(usuario.Nome, "Nome inválido"),
                    AssertionConcern.AssertNotEmpty(usuario.Login, "Login é obrigatório"),
                    AssertionConcern.AssertNotEmpty(usuario.Senha, "Senha é obrigatória")
                  );
        }

        public static bool AlterarUsuarioScopeSeValido(this Usuario usuario, string nome, string login, ICollection<Perfil> perfis)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(nome, "Nome é obrigatório"),
                    AssertionConcern.AssertNameIsValid(nome, "Nome inválido"),
                    AssertionConcern.AssertNotEmpty(login, "Login é obrigatório"),
                    AssertionConcern.AssertEmailIsValid(login, "Login não é e-mail válido")
                );
        }

        public static bool AlterarSenhaScopeSeValido(this Usuario usuario, string senha)
        {
            return AssertionConcern.IsValid(
                    AssertionConcern.AssertNotEmpty(senha, "Senha é obrigatório")
                  );
        }
    }
}
