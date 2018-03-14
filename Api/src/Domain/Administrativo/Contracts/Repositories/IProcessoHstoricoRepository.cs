using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IProcessoHstoricoRepository: IRepositoryBase<ProcessoHistorico>
    {
        ProcessoHistorico ObterHistoricoEmAberto(int processoId);
    }
}
