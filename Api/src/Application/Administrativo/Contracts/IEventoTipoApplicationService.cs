using System.Collections.Generic;
using Application.Administrativo.ViewModels.EventosTipos;

namespace Application.Administrativo.Contracts
{
    public interface IEventoTipoApplicationService
    {
        IEnumerable<EventoTipoVm> SelecionarTodosAtivos();
        void Registrar(EventoTipoVm model, string usuarioLogado);
        void Excluir(int id, string usuarioLogado);
        void Editar(EventoTipoVm model, string usuarioLogado);
        EventoTipoVm ObterPorId(int id);
    }
}
