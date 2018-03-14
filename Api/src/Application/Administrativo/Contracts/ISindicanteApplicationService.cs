using System.Collections.Generic;
using Application.Administrativo.ViewModels.Processos;
using Application.Administrativo.ViewModels.Sindicantes;

namespace Application.Administrativo.Contracts
{
    public interface ISindicanteApplicationService
    {
        void Registrar(SindicanteVm sindicanteVm, string user);
        void Editar(SindicanteVm sindicanteVm, string user);
        void Excluir(SindicanteDeleteVm sindicanteVm, string user);
        SindicanteEditVm ObterPorId(int id);
        IEnumerable<SindicanteVm> SelecionarTodosAtivos();
        IEnumerable<SindicanteVm> SelecionarTodosExternosAtivos();
        IEnumerable<ProcessoListVm> ObterProcessos(int sindicanteId);
        IEnumerable<SindicanteVm> SelecionarPorProcesso(int processoId);
    }
}
