using System.Collections.Generic;
using Domain.Account.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Account.Contracts.Repositories
{
    public interface IUsuarioRepository: IRepositoryBase<Usuario>
    {
        Usuario Autenticar(string email, string senha);
        IEnumerable<Usuario> SelecionarTodos();
        Usuario ObterPorId(int id);
        bool VerificarSeLoginExiste(string login);
        IEnumerable<Perfil> SelecionarPerfis(int usuarioId);
        bool VerificarLoginSenha(string login, string senha);
        bool VerificarSenhaPadrao(int id);
    }
}
