using System.Collections.Generic;
using Application.Administrativo.ViewModels.ServicosSeguradoras;

namespace Application.Administrativo.Contracts
{
    public interface IServicoSeguradoraApplicationService
    {
        void Registrar(ServicoSeguradoraVm servicoSeguradora, string user);
        void Alterar(ServicoSeguradoraVm servicoSeguradora, string user);
        IEnumerable<ServicoSeguradoraListVm> SelecionarTodosAtivos();
        IEnumerable<ServicoSeguradoraVm> SelecionarTodosComEventos();
        ServicoSeguradoraVm ObterPorId(int id);
        void Excluir(int id, string usuarioLogado);
    }
}
