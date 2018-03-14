using System.Collections.Generic;
using Application.Administrativo.ViewModels.Seguradoras;

namespace Application.Administrativo.Contracts
{
    public interface ISeguradoraApplicationService
    {
        void Registrar(SeguradoraVm model, string user);
        void Editar(SeguradoraVm model, string user);
        void Excluir(int id, string user);
        IEnumerable<SeguradoraVm> SelecionarTodosAtivos();
        SeguradoraVm ObterPorId(int id);

    }
}
