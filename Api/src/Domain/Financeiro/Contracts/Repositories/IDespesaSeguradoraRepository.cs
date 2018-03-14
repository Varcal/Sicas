using Domain.Core.Contracts.Repositories.Base;
using Domain.Financeiro.Entities.DespesasSeguradora;

namespace Domain.Financeiro.Contracts.Repositories
{
    public interface IDespesaSeguradoraRepository: IRepositoryBase<DespesaSeguradora>
    {
        DespesaSeguradora SelecionarPorProcesso(int processoId);
    }
}
