using System.Collections.Generic;
using Application.Core.ViewModels;

namespace Application.Core.Contracts
{
    public interface IEstadoApplicationService
    {
        IEnumerable<EstadoVm> SelecionarTodos();
    }
}
