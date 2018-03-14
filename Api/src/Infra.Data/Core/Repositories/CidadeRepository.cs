using System.Collections.Generic;
using System.Linq;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Entities.Enderecos;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Core.Repositories
{
    public class CidadeRepository : RepositoryBase<Cidade>, ICidadeRepository
    {
        public CidadeRepository(EfContext context)
            : base(context)
        {
            Context = context;
        }

        public IEnumerable<Cidade> SelecionarPorEstado(int ufId)
        {
            return Context.Cidades.Where(p => p.EstadoId == ufId).ToList();
        }
    }
}