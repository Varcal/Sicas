using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IServicoSindicanteRepository: IRepositoryBase<ServicoSindicante>
    {
        IEnumerable<ServicoSindicante> SelecionarTodos();
        IEnumerable<ServicoSindicante> SelecionarPorSindicante(int id);
    }
}
