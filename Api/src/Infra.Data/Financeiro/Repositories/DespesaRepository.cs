using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Financeiro.Repositories
{
    public class DespesaRepository : RepositoryBase<Despesa>, IDespesaRepository
    {
        public DespesaRepository(EfContext context)
            : base(context)
        {
            Context = context;
        }

        public Despesa GetById(int id)
        {
            return Context.Despesas.Where(x => x.Id == id)
                .Include(x => x.Origem.Endereco)
                .Include(x => x.Destino.Endereco)
                .Include(x => x.DespesasAdicionais)
                .FirstOrDefault();
        }


        public override void Update(Despesa entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public override void Delete(Despesa entity)
        {
            Context.Despesas.Remove(entity);
        }

        public void ExcluirDespesasAdicionais(IEnumerable<DespesaAdicional> despesasAdicionais)
        {
            foreach (var despesasAdicional in despesasAdicionais.ToList())
            {
                Context.Entry(despesasAdicional).State = EntityState.Deleted;
            }

        }

        public IEnumerable<DespesaAdicional> RetornarDespesasAdicionais(int despesaId)
        {
            return Context.DespesasAdicionais.Where(x => x.DespesaId == despesaId);
        }
    }
}
