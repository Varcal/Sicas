using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class EventoTipoRepository:RepositoryBase<EventoTipo>, IEventoTipoRepository
    {
        

        public EventoTipoRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public EventoTipo ObterPorId(int id)
        {
            return Context.EventosTipos.Find(id);
        }

        public IEnumerable<EventoTipo> SelecionarPorIds(IList<int> ids)
        {
            return Context.EventosTipos.Where(p => ids.Contains(p.Id)).ToList();
        }
    }
}
