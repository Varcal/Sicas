using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Financeiro.Repositories
{
    public class ReciboSindicanteRepository: RepositoryBase<ReciboSindicante>, IReciboSindicanteRepository
    {
        public ReciboSindicanteRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public ReciboSindicante ObterPorId(int id)
        {
            return Context.RecibosSindicantes.Include(x=>x.ReciboSindicanteItems).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ReciboSindicante> SelecionarPorSindicanteData(DateTime dataInicio, DateTime dataFim)
        {
            var endDate = dataFim.AddDays(1).AddMinutes(-1);

            return Context.RecibosSindicantes.Include(r => r.ReciboSindicanteItems).Where(r => r.DataInicio >= dataInicio && r.DataInicio <= endDate).ToList();
        }

        public IEnumerable<ReciboSindicante> SelecionarTodosNaoEmitidosPorData(DateTime dataInicio, DateTime dataFim)
        {
            var endDate = dataFim.AddDays(1).AddSeconds(-1);

            return Context.RecibosSindicantes.Include(r => r.ReciboSindicanteItems).Where(r => r.DataInicio >= dataInicio && r.DataInicio <= endDate && !r.Emitido).ToList();
        }
    }
}
