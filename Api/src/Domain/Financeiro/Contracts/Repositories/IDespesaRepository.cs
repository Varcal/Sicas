using System.Collections.Generic;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Financeiro.Entities.DespesasSeguradora;

namespace Domain.Financeiro.Contracts.Repositories
{
    public interface IDespesaRepository : IRepositoryBase<Despesa>
    {
        Despesa GetById(int id);
        void ExcluirDespesasAdicionais(IEnumerable<DespesaAdicional> despesasAdicionais);
        IEnumerable<DespesaAdicional> RetornarDespesasAdicionais(int despesaId);
    }
}
