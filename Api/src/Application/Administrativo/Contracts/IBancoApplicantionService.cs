using System.Collections.Generic;
using Application.Administrativo.ViewModels.Sindicantes;

namespace Application.Administrativo.Contracts
{
    public interface IBancoApplicantionService
    {
        IEnumerable<BancoVm> SelecionarTodosAtivos();
    }
}
