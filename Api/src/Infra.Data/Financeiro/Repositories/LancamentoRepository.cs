using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;
using System.Data.Entity;
using SharedKernel.Enums;

namespace Infra.Data.Financeiro.Repositories
{
    public class LancamentoRepository: RepositoryBase<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public override void Delete(Lancamento entity)
        {
            Context.Lancamentos.Remove(entity);
        }

        public Lancamento ObterPorId(int id)
        {
            return Context.Lancamentos.Find(id);
        }

        public IEnumerable<Recibo> SelecionarPorSindicanteData(int sindicanteId, DateTime dataInicio, DateTime dataFim)
        {
            var endDate = dataFim.AddDays(1).AddMinutes(-1);

            var processosIds = Context.Sindicantes.Where(x=>x.Id == sindicanteId).SelectMany(x=>x.Processos).Where(p=>p.FinalizadaAnaliseEm >= dataInicio && p.FinalizadaAnaliseEm < endDate).Select(x=>x.Id).ToList();

            return Context.Recibos
                .Include(x=>x.Processo.Seguradora.DadosPessoaJuridica)
                .Include(x=>x.Lancamentos)
                .Where(x => processosIds.Contains(x.ProcessoId) && x.SindicanteId == sindicanteId && (x.Status == StatusRecibo.Fechado || x.Status == StatusRecibo.Emitido)).ToList();
        }
    }
}
