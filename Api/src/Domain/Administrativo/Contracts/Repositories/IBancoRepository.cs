using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IBancoRepository: IRepositoryBase<Banco>
    {
        IEnumerable<Banco> SelecionarTodos();
    }
}
