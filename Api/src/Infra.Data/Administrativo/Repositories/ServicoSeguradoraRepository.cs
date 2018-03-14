using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class ServicoSeguradoraRepository:RepositoryBase<ServicoSeguradora>, IServicoSeguradoraRepository
    {
        public ServicoSeguradoraRepository(EfContext context) 
            : base(context)
        {
            Context = context;
        }

        public ServicoSeguradora ObterPorId(int id)
        {
            return Context.ServicosSeguradoras.Include(x => x.EventoTipos).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ServicoSeguradora> SelecionarTodosComEventos()
        {
            return Get().Where(x => x.Ativo).Include(x => x.EventoTipos).ToList();
        }       
    }
}
