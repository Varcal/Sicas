using System.Collections.Generic;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Core.Entities.Enderecos;

namespace Domain.Core.Contracts.Repositories
{
    public interface ICidadeRepository: IRepositoryBase<Cidade>
    {
        IEnumerable<Cidade> SelecionarPorEstado(int ufId);
    }
}
