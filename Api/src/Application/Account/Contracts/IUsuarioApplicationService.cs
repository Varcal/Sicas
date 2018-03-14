using System.Collections.Generic;
using Application.Account.ViewModels.Usuarios;

namespace Application.Account.Contracts
{
    public interface IUsuarioApplicationService
    {
        UsuarioVm Autenticar(string login, string senha);
        bool Registrar(UsuarioVm usuario, string user);
        bool Alterar(UsuarioVm usuario, string user);
        bool Excluir(int id, string user);
        UsuarioVm ObterPorId(int id);
        UsuarioLogadoVm ObterUsuario(string login, string senha);
        IEnumerable<UsuarioVm> SelecionarTodosAtivos();
        bool VerificarSeLoginExiste(string login);
        bool VerificarLoginSenha(string login, string senha);
        void AlterarSenha(UsuarioVm model, string usuarioLogado);
        bool VerificarSenhaPadrao(int id);
        void ResetarSenha(int usuarioId, string usuarioLogado);
    }
}
