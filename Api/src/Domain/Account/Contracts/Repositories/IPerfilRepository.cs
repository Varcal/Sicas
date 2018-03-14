using System.Collections.Generic;
using Domain.Account.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Account.Contracts.Repositories
{
    public interface IPerfilRepository: IRepositoryBase<Perfil>
    {
        Perfil ObterPerfil(int id);
        IEnumerable<Perfil> SelecionarTodos();
        IEnumerable<Perfil> SelecionarPorIds(IList<int> ids);
    }
}
