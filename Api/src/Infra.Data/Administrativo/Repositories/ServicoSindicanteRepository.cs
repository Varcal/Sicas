using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class ServicoSindicanteRepository: RepositoryBase<ServicoSindicante>, IServicoSindicanteRepository
    {
        public ServicoSindicanteRepository(EfContext context) : base(context)
        {
            Context = context;
        }

        public IEnumerable<ServicoSindicante> SelecionarTodos()
        {
            return Context.ServicosSindicantes.OrderBy(p=>p.Nome).ToList();
        }

        public IEnumerable<ServicoSindicante> SelecionarPorSindicante(int id)
        {
            var servicos = Context.Honorarios.Where(p => p.SindicanteId == id).Select(p => p.ServicoSindicante).Distinct().ToList();

            return servicos;
        }
    }
}
