using System.Collections.Generic;
using Application.Account.ViewModels.Perfis;

namespace Application.Account.Contracts
{
    public interface IPerfilApplicationService
    {
        IEnumerable<PerfilVm> SelecionarTodosAtivos();
    }
}
