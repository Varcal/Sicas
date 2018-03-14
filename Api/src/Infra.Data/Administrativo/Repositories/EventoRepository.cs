using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class EventoRepository: RepositoryBase<Evento>, IEventoRepository
    {
        public EventoRepository(EfContext context) : base(context)
        {
            Context = context;
        }

        public int ObterFranquiaKm(int seguradoraId, int servicoSeguradoraId, int tipoEvento)
        {
            return Context.Eventos.Where(x => x.SeguradoraId == seguradoraId && x.ServicoSeguradoraId == servicoSeguradoraId && x.EventoTipoId == tipoEvento)
                .Select(x => x.FranquiaKm)
                .SingleOrDefault();
        }
    }
}
