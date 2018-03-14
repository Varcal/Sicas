using System.Collections.Generic;
using System.Linq;
using Domain.Financeiro.Contracts.Repositories;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;
using System.Data.Entity;
using Domain.Financeiro.Entities.RecibosSindicantes;
using SharedKernel.Enums;

namespace Infra.Data.Financeiro.Repositories
{
    public class ReciboRepository: RepositoryBase<Recibo>, IReciboRepository
    {
        public ReciboRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public Recibo ObterRecibo(int sindicanteId, int processoId)
        {
            return Context.Recibos.Include(x=>x.Lancamentos).SingleOrDefault(x => x.SindicanteId == sindicanteId && x.ProcessoId == processoId);
        }

        public Recibo ObterId(int id)
        {
            return Context.Recibos.Find(id);
        }

        public IEnumerable<Recibo> SelecionarPorIds(List<int> recibosIds)
        {
            return Context.Recibos
                .Include(x=>x.Sindicante.DadosPessoaFisica)
                .Include(x => x.Sindicante.DadosBancarios.Banco)
                .Include(x=>x.Processo.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Processo.Segurado)
                .Include(x=>x.Lancamentos)
                .Where(x => recibosIds.Contains(x.Id) && x.Status == StatusRecibo.Fechado).ToList();
        }
    }
}
