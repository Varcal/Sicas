using System.Collections.Generic;
using Application.Core.ViewModels;

namespace Application.Core.Contracts
{
    public interface ICidadeApplicationService
    {
        IEnumerable<CidadeVm> SelecionarPorEstado(int ufId);
    }
}
