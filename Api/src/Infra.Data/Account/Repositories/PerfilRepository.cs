using System.Collections.Generic;
using System.Linq;
using Domain.Account.Contracts.Repositories;
using Domain.Account.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Account.Repositories
{
    public class PerfilRepository : RepositoryBase<Perfil>, IPerfilRepository
    {
        public PerfilRepository(EfContext context) : base(context)
        {
            Context = context;
        }


        public Perfil ObterPerfil(int id)
        {
            return Context.Perfis.Find(id);
        }

        public IEnumerable<Perfil> SelecionarPorIds(IList<int> ids)
        {
            return Context.Perfis.Where(p => ids.Contains(p.Id)).ToList();
        }
        
        public IEnumerable<Perfil> SelecionarTodos()
        {
            return Context.Perfis.ToList();
        }
    }
}
