using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Contexts;
using System.Data.Entity;
using Domain.Account.Entities;
using Infra.Data.Core.Repositories.Base;
using SharedKernel.Enums;

namespace Infra.Data.Administrativo.Repositories
{
    public class SindicanteRepository: RepositoryBase<Sindicante>, ISindicanteRepository
    {
        public SindicanteRepository(EfContext context) 
            :base(context)
        {
            Context = context;
        }

        public Sindicante ObterPorId(int id)
        {
            return Context.Sindicantes.Where(p=>p.Id == id)
                .Include(x=>x.SindicanteTipo)
                .Include(x=>x.DadosPessoaFisica)
                .Include(x=>x.DadosBancarios)
                .Include(x=>x.Enderecos.Select(e=>e.Endereco))
                .Include(x=>x.Honorarios.Select(h => h.ServicoSeguradora))
                .Include(x => x.Honorarios.Select(h => h.EventoTipo))
                .Include(x => x.Honorarios.Select(h => h.ServicoSindicante))
                .SingleOrDefault();
        }

        public Sindicante ObterPorIdParaLancamento(int id)
        {
            return Context.Sindicantes.Where(p => p.Id == id)
                .Include(x => x.SindicanteTipo)
                .Include(x => x.DadosPessoaFisica)
                .Include(x => x.DadosBancarios)
                .Include(x => x.Enderecos.Select(e => e.Endereco))
                .Include(x => x.Honorarios.Select(h => h.ServicoSeguradora))
                .Include(x => x.Honorarios.Select(h => h.EventoTipo))
                .Include(x => x.Honorarios.Select(h => h.ServicoSindicante))
                .Include(x=>x.Processos.Select(p => p.Seguradora))
                .SingleOrDefault();
        }


        public SindicanteInterno ObterInternoPorId(int id)
        {
            return Context.SindicantesInternos.Where(p => p.Id == id)
                .Include(x=>x.Usuario.Perfis)
                .Include(x => x.SindicanteTipo)
                .Include(x => x.DadosBancarios)
                .Include(x=>x.DadosPessoaFisica)
                .Include(x => x.Enderecos.Select(e => e.Endereco))
                .Include(x => x.Honorarios.Select(h => h.ServicoSeguradora))
                .Include(x => x.Honorarios.Select(h => h.EventoTipo))
                .Include(x => x.Honorarios.Select(h => h.ServicoSindicante))
                .SingleOrDefault();
        }

        public IEnumerable<Sindicante> SelecionarTodos()
        {
            return Context.Sindicantes.Where(x=>x.Ativo).Include(p=>p.SindicanteTipo).Include(p=>p.DadosPessoaFisica).OrderBy(p=>p.DadosPessoaFisica.Nome).ToList();
        }

        public override IEnumerable<Sindicante> GetAllActive()
        {
            return Context.Sindicantes.Where(p=>p.Ativo)
                .Include(p => p.SindicanteTipo)
                .Include(p => p.DadosPessoaFisica)
                .OrderBy(p => p.DadosPessoaFisica.Nome).ToList();
        }

        public Usuario RetornarUsuario(int sindicanteId)
        {
            return Context.SindicantesInternos.Where(x=>x.Id == sindicanteId).Select(x => x.Usuario).SingleOrDefault();
        }

        public IEnumerable<Sindicante> SelecionarTodosExternosAtivos()
        {
            return Context.Sindicantes.Where(x=>x.SindicanteTipoId == (int)SindicanteTipoEnum.Externo && x.Ativo)
                .Include(p => p.SindicanteTipo)
                .Include(p => p.DadosPessoaFisica)
                .OrderBy(p => p.DadosPessoaFisica.Nome).ToList();

        }

        public IEnumerable<Processo> ObterProcessos(int sindicanteId)
        {
            return Context.Sindicantes              
                .Where(x => x.Id == sindicanteId)
                .SelectMany(x=>x.Processos)
                .Include(s =>  s.Seguradora.DadosPessoaJuridica)
                .Include(s => s.Sindicantes.Select(d => d.DadosPessoaFisica)).ToList();
        }

        public IEnumerable<Sindicante> SelecionarPorProcesso(int processoId)
        {
            return Context.Processos.Where(p=>p.Id == processoId).SelectMany(p => p.Sindicantes)
                .Include(s=> s.SindicanteTipo)
                .Include(s => s.DadosPessoaFisica).ToList();
        }

        public bool VerificarSeSindicanteInterno(int id)
        {
            return Context.Sindicantes.Where(x=>x.Id == id).Select(x => x.SindicanteTipoId).FirstOrDefault() == (int) SindicanteTipoEnum.Interno;
        }
    }
}
