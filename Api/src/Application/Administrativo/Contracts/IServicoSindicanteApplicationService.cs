using System.Collections.Generic;
using Application.Administrativo.ViewModels.ServicosSindicantes;

namespace Application.Administrativo.Contracts
{
    public interface IServicoSindicanteApplicationService
    {
        IEnumerable<ServicoSindicanteVm> SelecionarTodosAtivos();
        IEnumerable<ServicoSindicanteVm> SelecionarPorSindicante(int id);
    }
}
