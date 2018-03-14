using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;

namespace Infra.Data.Administrativo.Repositories
{
    public class SeguradoraRepository: RepositoryBase<Seguradora>, ISeguradoraRepository
    {
        public SeguradoraRepository(EfContext context) : base(context)
        {
            Context = context;
        }

        public override IEnumerable<Seguradora> GetAllActive()
        {
            return Context.Seguradoras.Where(x => x.Ativo)
                .Include(p=>p.DadosPessoaJuridica)
                .Include(x => x.Eventos)
                .OrderBy(x => x.DadosPessoaJuridica.RazaoSocial).ToList();
        }

        public Seguradora GetById(int id)
        {
            return Context.Seguradoras.Where(x=>x.Id == id)
                .Include(p=>p.DadosPessoaJuridica)
                .Include(x=>x.Eventos)
                .Include(x=>x.Eventos.Select(e => e.EventoTipo))
                .Include(x=>x.Eventos.Select(s=>s.ServicoSeguradora))
                .SingleOrDefault();
        }

        public string RetornaUrlAdministrativo(int seguradoraId)
        {
            return Context.Seguradoras.Where(x => x.Id == seguradoraId).Select(x => x.UrlAdministrativo).SingleOrDefault();
        }
    }
}
