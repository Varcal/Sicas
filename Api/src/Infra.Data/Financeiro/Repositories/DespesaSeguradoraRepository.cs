using System.Linq;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;
using System.Data.Entity;

namespace Infra.Data.Financeiro.Repositories
{
    public class DespesaSeguradoraRepository: RepositoryBase<DespesaSeguradora>, IDespesaSeguradoraRepository
    {
        public DespesaSeguradoraRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public DespesaSeguradora SelecionarPorProcesso(int processoId)
        {
            return Context.DespesasSeguradoras.Include(x=>x.DespesasSeguradoraItems).SingleOrDefault(x => x.ProcessoId == processoId);
        }
    }
}
