using System.Collections.Generic;
using System.Linq;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Entities.Enderecos;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Core.Repositories
{
    public class EstadoRepository: RepositoryBase<Estado>, IEstadoRepository
    {
        public EstadoRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public IEnumerable<Estado> SelecionarTodos()
        {
            return Context.Estados.ToList();
        }
    }
}
