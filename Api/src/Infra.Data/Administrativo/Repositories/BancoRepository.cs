using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class BancoRepository: RepositoryBase<Banco>, IBancoRepository
    {
        public BancoRepository(EfContext context) : base(context)
        {
            Context = context;
        }

        public IEnumerable<Banco> SelecionarTodos()
        {
            return Context.Bancos.OrderBy(p => p.Nome).ToList();
        }
    }
}
