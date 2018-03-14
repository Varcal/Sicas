using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class ProcessoHistoricoRepository: RepositoryBase<ProcessoHistorico>, IProcessoHstoricoRepository
    {
        public ProcessoHistoricoRepository(EfContext context) : 
            base(context)
        {
            Context = context;
        }

        public ProcessoHistorico ObterHistoricoEmAberto(int processoId)
        {
            return Context.ProcessosHistoricos.SingleOrDefault(x => x.ProcessoId == processoId && x.FinalizadoEm == null);
        }
    }
}
