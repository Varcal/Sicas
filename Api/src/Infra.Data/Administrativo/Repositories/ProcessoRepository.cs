using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using Domain.Account.Entities;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Domain.Administrativo.Specs;
using Domain.Dtos;
using Infra.Data.Contexts;
using Infra.Data.Core.Repositories.Base;
using SharedKernel.Enums;

namespace Infra.Data.Administrativo.Repositories
{
    public class ProcessoRepository : RepositoryBase<Processo>, IProcessoRepository
    {
        public ProcessoRepository(EfContext context) : base(context)
        {
            Context = context;
        }

        public IEnumerable<Processo> SelecionarTodos()
        {
            return Context.Processos.ToList();
        }

        public IEnumerable<Processo> SelecionarPorSeguradoraCombo(int seguradoraId)
        {
            return Context.Processos.Where(x => x.SeguradoraId == seguradoraId && (x.StatusId == (int)StatusEnum.AguardandoEmissaoNf || x.StatusId == (int)StatusEnum.EnviadoSeguradora || x.StatusId == (int)StatusEnum.Finalizado))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.EventoTipo)
                .Include(x => x.ServicoSeguradora)
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .ToList();
        }

        public IEnumerable<Processo> SelecionarProcessos(int seguradoraId, int statusId, Perfil perfil, [Optional]int? usuarioId)
        {
            const int todos = 0;

            var query = Context.Processos
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.EventoTipo)
                .Include(x => x.ServicoSeguradora)
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .AsQueryable();

            if (seguradoraId != todos)
                query = query.Where(x => x.SeguradoraId == seguradoraId);

            if (usuarioId.HasValue)
                query = query.Where(x => x.UsuarioResponsavelId == usuarioId && (x.StatusId == (int)StatusEnum.EmAnalise || x.StatusId == (int)StatusEnum.EnviadoSindicanteExterno));

            query = statusId != todos ? query.Where(x => x.StatusId == statusId) : query.Where(ProcessoSpecs.SelecionarTodos());

            //if (perfil.Id == (int)PerfilEnum.Analista)
            //    query.Where(x => x.StatusId == (int)StatusEnum.EmAnalise || x.StatusId == (int)StatusEnum.EnviadoSindicanteExterno);


            return query.OrderBy(x=>x.DataCadastro).ToList();
        }

        public Processo ObterPorId(int id)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorId(id))
                .Include(x => x.Sindicantes.Select(s => s.SindicanteTipo))
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Sindicantes.Select(s => s.Honorarios))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Despesas.Select(da => da.DespesasAdicionais))
                .Include(x => x.Despesas.Select(e => e.Origem.Endereco.Cidade.Estado))
                .Include(x => x.Despesas.Select(e => e.Destino.Endereco.Cidade.Estado))
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .FirstOrDefault();
        }

        public Processo ObterPorIdComAcionamento(int id)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorId(id))
                .Include(x => x.Sindicantes.Select(s => s.SindicanteTipo))
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Sindicantes.Select(s => s.Honorarios))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Despesas.Select(da => da.DespesasAdicionais))
                .Include(x => x.Despesas.Select(e => e.Origem.Endereco.Cidade.Estado))
                .Include(x => x.Despesas.Select(e => e.Destino.Endereco.Cidade.Estado))
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Acionamento)
                .FirstOrDefault();
        }

        public Processo ObterParaParecer(int id)
        {
            return Context.Processos.Where(x => x.Id == id)
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.ServicoSeguradora)
                .Include(x => x.ServicoSindicante)
                .Include(x => x.EventoTipo)
                .Include(x => x.Historicos)
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Sindicantes.Select(p => p.DadosPessoaFisica)).FirstOrDefault();
        }

        public Processo ObterParaDespesa(int id)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorId(id))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Despesas.Select(da => da.DespesasAdicionais))
                .Include(x => x.Despesas.Select(e => e.Origem.Endereco.Cidade.Estado))
                .Include(x => x.Despesas.Select(e => e.Destino.Endereco.Cidade.Estado))
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .FirstOrDefault();
        }

        public Processo SelecionarComHistorico(int seguradoraId, string numeroSinistro)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorSeguradoraIdNumeroSinistro(seguradoraId, numeroSinistro))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Historicos.Select(s => s.Status))
                .Include(x => x.Segurado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .SingleOrDefault();
        }

        public Processo SelecionarComHistoricoFinanceiro(int seguradoraId, string numeroSinistro)
        {
            return Context.Processos.Where(x => x.SeguradoraId == seguradoraId && x.NumeroSinistro == numeroSinistro && (x.StatusId == (int)StatusEnum.EnviadoFinanceiro || x.StatusId == (int)StatusEnum.AguardandoEmissaoNf || x.StatusId == (int)StatusEnum.Finalizado))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Historicos.Select(s => s.Status))
                .Include(x => x.Segurado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .SingleOrDefault();
        }

        public Processo SelecionarComHistorico(int seguradoraId, string numeroSinistro, int usuarioId)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorIdSinistroUsuario(seguradoraId, numeroSinistro, usuarioId))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Historicos.Select(s => s.Status))
                .Include(x => x.Segurado)
                .Include(x => x.Cidade.Estado)
                .SingleOrDefault();
        }

        public IEnumerable<Processo> SelecionarPorSeguradoraParaDespesas(int? seguradoraId)
        {
            var query = Context.Processos.Where(ProcessoSpecs.SelecionarTodosParaEmitirNf())
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.EventoTipo)
                .Include(x => x.ServicoSeguradora)
                .Include(x => x.Despesas)
                .Include(x => x.Segurado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado).AsQueryable();

            if (seguradoraId.HasValue)
            {
                query = query.Where(x => x.SeguradoraId == seguradoraId.Value);
            }

            return query.ToList();
        }

        public IEnumerable<Processo> SelecionarPorSeguradoraData(int seguradoraId, DateTime dataInicio, DateTime dataFim)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorSeguradoraData(seguradoraId, dataInicio, dataFim))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Segurado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .ToList();
        }

        public Processo SelecionarProcessoParaRecibo(int processoId)
        {
            return Context.Processos.Where(ProcessoSpecs.SelecionarPorId(processoId))
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(st => st.SindicanteTipo))
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.Despesas.Select(d => d.Destino.Endereco.Cidade.Estado))
                .Include(x => x.Segurado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Despesas.Select(da => da.DespesasAdicionais)).SingleOrDefault();
        }

        public IEnumerable<Processo> SelecionarPorSeguradoraFinanceiro(int seguradoraId)
        {
            return Context.Processos.Where(x => x.SeguradoraId == seguradoraId && (x.StatusId == (int)StatusEnum.EnviadoFinanceiro || x.StatusId == (int)StatusEnum.AguardandoEmissaoNf || x.StatusId == (int)StatusEnum.Finalizado))
               .Include(x => x.Seguradora.DadosPessoaJuridica)
               .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
               .Include(x => x.EventoTipo)
               .Include(x => x.Segurado)
               //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
               .Include(x => x.Cidade.Estado)
               .Include(x => x.ServicoSeguradora).ToList();
        }

        public Acionamento ObterAcionamento(int id)
        {
            return Context.Processos.Where(x => x.AcionamentoId == id).Select(x => x.Acionamento).SingleOrDefault();
        }

        public IEnumerable<Processo> SelecionarTodos(Perfil perfil, [Optional]int? usuarioId)
        {
            var query = Context.Processos.Include(x => x.Seguradora.DadosPessoaJuridica)
               .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
               .Include(x => x.EventoTipo)
               .Include(x => x.Segurado)
               //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
               .Include(x => x.Cidade.Estado)
               .Include(x => x.ServicoSeguradora).AsQueryable();


            if (usuarioId.HasValue)
                query = query.Where(x => x.UsuarioResponsavelId == usuarioId);


            switch ((PerfilEnum)perfil.Id)
            {
                case PerfilEnum.Financeiro:
                    query = query.Where(x => x.StatusId == (int)StatusEnum.EnviadoFinanceiro || x.StatusId == (int)StatusEnum.AguardandoEmissaoNf);
                    break;
                case PerfilEnum.Gestor:
                    query = query.Where(x => x.StatusId == (int)StatusEnum.EmAnalise || x.StatusId == (int)StatusEnum.FinalizadaAnalise);
                    break;
                case PerfilEnum.Analista:
                    query = query.Where(x => x.StatusId == (int)StatusEnum.EmAnalise);
                    break;
            }

            return query.ToList();
        }

        public AlertaDto ObterAlerta(int? usuarioId)
        {

            var totalMaisDe7Dias = Context.Processos.Where(ProcessoSpecs.AbertoMais7Dias(usuarioId)).Count();
            var totalMaisDe5Dias = Context.Processos.Where(ProcessoSpecs.AbertoDe5A7Dias(usuarioId)).Count();
            var totalAguardandoParecer = !usuarioId.HasValue ? Context.Processos.Where(ProcessoSpecs.AguardandoParecer()).Count() : 0;
            var totalFinalizadosNoMes = Context.Processos.Where(ProcessoSpecs.Finalizados(usuarioId)).Count();


            return new AlertaDto(totalMaisDe7Dias, totalMaisDe5Dias, totalAguardandoParecer, totalFinalizadosNoMes);
        }

        public AlertaFinanceiroDto ObterAlertaFinanceiro()
        {
            

            var totalEnvidadoFinanceiro = Context.Processos.Where(ProcessoSpecs.EnviadoParaFinanceiro()).Count();
            var totalAguardandoEmissaoNf = Context.Processos.Where(ProcessoSpecs.AguardandoEmitirNf()).Count();
            var totalFinalizadosNoMes = Context.Processos.Where(ProcessoSpecs.Finalizados()).Count();

            var totalEnviadoParaSeguradora = ProcessosParaSeguradora().Count();


            return new AlertaFinanceiroDto(totalEnvidadoFinanceiro, totalAguardandoEmissaoNf, totalEnviadoParaSeguradora, totalFinalizadosNoMes);

        }

        public IEnumerable<SindicanteHomeDto> SelecionarSindicantesStatusMensal()
        {
            var inicioPrimeiraQuinzena = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var fimPrimeiraQuinzena = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16).AddSeconds(-1);
            var inicioSegundaQuinzena = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 16);
            var fimSegundaQuinzena = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);

            var sindicantesInternos = Context.SindicantesInternos
                .Select(s => new
                {
                    Nome = s.DadosPessoaFisica.Nome,
                    PrimeiraQuinzena = s.Processos.Count(x => x.DataCadastro >= inicioPrimeiraQuinzena && x.DataCadastro <= fimPrimeiraQuinzena),
                    SegundaQuinzena = s.Processos.Count(x => x.DataCadastro >= inicioSegundaQuinzena && x.DataCadastro <= fimSegundaQuinzena)
                }).ToList();


            return sindicantesInternos.Select(x => new SindicanteHomeDto(x.Nome, x.PrimeiraQuinzena, x.SegundaQuinzena)).ToList();
        }

        public IEnumerable<Processo> SelecionarPorAlerta(int alerta, Perfil perfil, [Optional]int? usuarioId)
        {
            var query = Context.Processos
                .Include(x => x.Seguradora.DadosPessoaJuridica)
                .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                .Include(x => x.EventoTipo)
                .Include(x => x.ServicoSeguradora)
                .Include(x => x.Cidade.Estado)
                .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado)
                //.Include(x => x.LocalFatos.Endereco.Cidade.Estado)
                .AsQueryable();

            if (alerta == 1)
                query = query.Where(ProcessoSpecs.AbertoMais7Dias(usuarioId));
            if (alerta == 2)
                query = query.Where(ProcessoSpecs.AbertoDe5A7Dias(usuarioId));
            if (alerta == 3 && !usuarioId.HasValue)
                query = query.Where(ProcessoSpecs.AguardandoParecer()); 
            if (alerta == 4)
                query = query.Where(ProcessoSpecs.Finalizados(usuarioId));
            if (alerta == 5)
                query = query.Where(ProcessoSpecs.EnviadoParaFinanceiro());
            if (alerta == 6)
                query = query.Where(ProcessoSpecs.AguardandoEmitirNf());
            if (alerta == 7 && !usuarioId.HasValue)
                return ProcessosParaSeguradora().Include(x => x.Seguradora.DadosPessoaJuridica)
                            .Include(x => x.Sindicantes.Select(s => s.DadosPessoaFisica))
                            .Include(x => x.EventoTipo)
                            .Include(x => x.ServicoSeguradora)
                            .Include(x => x.Cidade.Estado)
                            .Include(x => x.Segurado.EnderecoSegurado.Endereco.Cidade.Estado).ToList();
            if (alerta == 8)
                query = query.Where(ProcessoSpecs.Finalizados());

            return query.ToList();
        }

        private IQueryable<Processo> ProcessosParaSeguradora()
        {
            var dataInicio = new DateTime(DateTime.Today.Year, DateTime.Now.Month, 1);
            var dataFim = new DateTime(DateTime.Today.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1);

            var processos = (from processo in Context.Processos
                                              join historico in Context.ProcessosHistoricos on processo.Id equals historico.ProcessoId
                                              where historico.StatusId == (int)StatusEnum.EnviadoSeguradora 
                                              && (historico.IniciadoEm >= dataInicio && historico.IniciadoEm <= dataFim)
                                              select processo).AsQueryable();

            return processos;
        }
    }
}
