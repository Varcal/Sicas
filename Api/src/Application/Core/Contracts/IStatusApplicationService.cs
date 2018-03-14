using System.Collections.Generic;
using Application.Core.ViewModels;

namespace Application.Core.Contracts
{
    public interface IStatusApplicationService
    {
        IEnumerable<StatusVm> SelecionarTodos();
    }
}
