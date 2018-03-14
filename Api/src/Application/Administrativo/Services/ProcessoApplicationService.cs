using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Processos;
using Application.Administrativo.ViewModels.Sindicantes;
using Application.Core.Services;
using CrossCutting.Utils.Enums;
using CrossCutting.Utils.Helpers;
using CrossCutting.Utils.Resources;
using Domain.Account.Contracts.Repositories;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Contracts.Services;
using Domain.Administrativo.Entities;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Contracts.Services;
using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.UnitOfWork;
using SharedKernel.DomainEvents;
using SharedKernel.DomainEvents.Events.Emails;
using SharedKernel.Enums;

namespace Application.Administrativo.Services
{
    public class ProcessoApplicationService : ApplicationService, IProcessoApplicationService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly ISindicanteRepository _sindicanteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProcessoHistoricoDomainService _processoHistoricoDomainService;
        private readonly ILancamentoDomainService _lancamentoDomainService;
        private readonly IDespesaSeguradoraRepository _despesaSeguradoraRepository;
        private readonly IEnderecoDomainService _enderecoDomainService;

        public ProcessoApplicationService(IUnitOfWork uow, IProcessoRepository processoRepository, ISindicanteRepository sindicanteRepository, IUsuarioRepository usuarioRepository, IProcessoHistoricoDomainService processoHistoricoDomainService, ILancamentoDomainService lancamentoDomainService, IDespesaSeguradoraRepository despesaSeguradoraRepository, IEnderecoDomainService enderecoDomainService) : base(uow)
        {
            _processoRepository = processoRepository;
            _sindicanteRepository = sindicanteRepository;
            _usuarioRepository = usuarioRepository;
            _processoHistoricoDomainService = processoHistoricoDomainService;
            _lancamentoDomainService = lancamentoDomainService;
            _despesaSeguradoraRepository = despesaSeguradoraRepository;
            _enderecoDomainService = enderecoDomainService;
        }


        public void Registrar(ProcessoCreateVm model, string usuarioLogado)
        {
            var usuario = _sindicanteRepository.RetornarUsuario(model.SindicanteId);
            var sindicante = _sindicanteRepository.ObterPorId(model.SindicanteId);
            //var enderecoSegurado = _enderecoDomainService.RetornaEnderecoSegurado(model.Segurado.EnderecoSegurado.Logradouro, model.Segurado.EnderecoSegurado.Numero, model.Segurado.EnderecoSegurado.Cep, model.Segurado.EnderecoSegurado.Bairro, model.Segurado.EnderecoSegurado.CidadeId, model.Segurado.EnderecoSegurado.Complemento);
            //var localFatos = _enderecoDomainService.RetornaLocalFatos(model.LocalFatos.Logradouro, model.LocalFatos.Numero, model.LocalFatos.Cep, model.LocalFatos.Bairro, model.LocalFatos.CidadeId, model.LocalFatos.Complemento);
            var segurado = new Segurado(model.Segurado.Nome);

            var processo = new Processo(model.DataCadastro, model.SeguradoraId, model.ServicoSeguradoraId, sindicante, model.EventoTipoId, usuario, model.ServicoSindicanteId, model.NumeroSinistro, model.Analista, model.Placa, model.CidadeId, segurado,
                //localFatos, 
                model.NumeroReferencia);

            if (model.Arquivo != null)
                processo.IncluirAcionamento(new Acionamento(processo.NumeroSinistro.PadLeft(10, '0'), model.Arquivo));

            _processoRepository.Add(processo);
            //_lancamentoDomainService.GerarLancamentoHonorario(processo, sindicante);

            Save(usuarioLogado);
        }

        public IEnumerable<ProcessoListVm> SelecionarPorSeguradora(int seguradoraId, int statusId, int alerta, int usuarioId)
        {
            var perfil = _usuarioRepository.SelecionarPerfis(usuarioId).First();
            var processos = new List<Processo>();


            if (alerta == 0)
            {
                processos = perfil.Id == (int) PerfilEnum.Analista
                    ? _processoRepository.SelecionarProcessos(seguradoraId, statusId, perfil, usuarioId).ToList()
                    : _processoRepository.SelecionarProcessos(seguradoraId, statusId, perfil).ToList();
            }
            else
            {
                processos = perfil.Id == (int)PerfilEnum.Analista
                    ? _processoRepository.SelecionarPorAlerta(alerta, perfil, usuarioId).ToList()
                    : _processoRepository.SelecionarPorAlerta(alerta, perfil).ToList();
            }


            return processos.Select(p => new ProcessoListVm(p));
        }


        public ProcessoEditVm ObterPorId(int id)
        {
            var processo = _processoRepository.ObterPorId(id);

            return new ProcessoEditVm(processo);
        }

        public void Editar(ProcessoEditVm model, string usuarioLogado)
        {

            var processo = _processoRepository.ObterPorIdComAcionamento(model.Id);
            var sindicante = _sindicanteRepository.VerificarSeSindicanteInterno(model.SindicanteId) ? _sindicanteRepository.ObterInternoPorId(model.SindicanteId) : _sindicanteRepository.ObterPorId(model.SindicanteId);
            //var localFatos = _enderecoDomainService.RetornaLocalFatos(model.LocalFatos.Logradouro, model.LocalFatos.Numero, model.LocalFatos.Cep, model.LocalFatos.Bairro, model.LocalFatos.CidadeId, model.LocalFatos.Complemento);
            //var enderecoSegurado = _enderecoDomainService.RetornaEnderecoSegurado(model.Segurado.EnderecoSegurado.Logradouro, model.Segurado.EnderecoSegurado.Numero, model.Segurado.EnderecoSegurado.Cep, model.Segurado.EnderecoSegurado.Bairro, model.Segurado.EnderecoSegurado.CidadeId, model.Segurado.EnderecoSegurado.Complemento);
            var segurado = new Segurado(model.Segurado.Nome);

            processo.Alterar(
                model.SeguradoraId,
                model.ServicoSeguradoraId,
                model.EventoTipoId,
                sindicante,
                model.ServicoSindicanteId,
                model.NumeroSinistro,
                model.NumeroReferencia,
                model.Analista,
                model.Placa,
                model.CidadeId,
                segurado,
                //localFatos,
                model.Arquivo
                );

            _processoRepository.Update(processo);

            Save(usuarioLogado);
        }

        public IEnumerable<ProcessoHistoricoVm> SelecionarHistoricos(int seguradoraId, string numeroSinistro, int usuarioId)
        {
            Processo processo;
            var perfis = _usuarioRepository.SelecionarPerfis(usuarioId).ToList();

            if (perfis.Select(x => x.Id).Contains((int)PerfilEnum.Administrador) ||
                perfis.Select(x => x.Id).Contains((int)PerfilEnum.Gestor) || perfis.Select(x => x.Id).Contains((int)PerfilEnum.Administrativo))
            {
                processo = _processoRepository.SelecionarComHistorico(seguradoraId, numeroSinistro);
            }
            else if (perfis.Select(x => x.Id).Contains((int)PerfilEnum.Financeiro))
            {
                processo = _processoRepository.SelecionarComHistoricoFinanceiro(seguradoraId, numeroSinistro);
            }
            else
            {
                processo = _processoRepository.SelecionarComHistorico(seguradoraId, numeroSinistro, usuarioId);
            }


            return ProcessoHistoricoVm.RetornaHistorico(processo);
        }

        public ProcessoParecerVm ObterParaParecer(int id)
        {
            var processo = _processoRepository.ObterParaParecer(id);

            return new ProcessoParecerVm(processo);
        }

        public void FinalizarParecer(ProcessoParecerVm model, string usuarioLogado)
        {
            var root = HttpRuntime.AppDomainAppPath + @"\Uploads";

            var processo = _processoRepository.ObterParaParecer(model.Id);

            _processoHistoricoDomainService.FinalizarHistorico(processo);

            processo.FinalizarParecer(model.Situacao, model.Comentario);

            var nomeArquivo = processo.NumeroSinistro.Replace(".", "").Trim();
            nomeArquivo = nomeArquivo.PadLeft(12, '0');

            var path = root + processo.Seguradora.UrlAdministrativo + @"\" + nomeArquivo + ".rar";

            model.Arquivos.SaveAs(path);

            _processoRepository.Update(processo);

            Save(usuarioLogado);
        }

        public void FinalizarAnalise(int id, string usuarioLogado)
        {
            var processo = _processoRepository.ObterPorId(id);

            _processoHistoricoDomainService.FinalizarHistorico(processo);

            processo.FinalizarAnalise();
            _processoRepository.Update(processo);
            _lancamentoDomainService.GerarLancamentoHonorario(processo, processo.Sindicantes.ToList());

            Save(usuarioLogado);
        }

        public void FinalizarDespesas(int id, string usuarioLogado)
        {
            var processo = _processoRepository.ObterPorId(id);

            _processoHistoricoDomainService.FinalizarHistorico(processo);

            processo.FinalizarDespesas();
            _processoRepository.Update(processo);

            var despesaSeguradora = new DespesaSeguradora(processo);
            _despesaSeguradoraRepository.Add(despesaSeguradora);

            Save(usuarioLogado);
        }

        public void CancelarProcesso(int id, string usuarioLogado)
        {
            var processo = _processoRepository.ObterPorId(id);

            _processoHistoricoDomainService.FinalizarHistorico(processo);

            processo.Cancelar();
            _processoRepository.Update(processo);

            Save(usuarioLogado);
        }

        public void EnviarSindicanteExterno(int id, int sindicanteExternoId, string usuarioLogado)
        {
            var sindicante = _sindicanteRepository.ObterPorId(sindicanteExternoId);
            var processo = _processoRepository.ObterPorIdComAcionamento(id);

            _processoHistoricoDomainService.FinalizarHistorico(processo);

            processo.EnviarSindicanteExterno(sindicante);

            _processoRepository.Update(processo);
            
            //Enviar email
            DomainEvent.Raise(CriarEmailEnvioSindicanteExterno(processo));

            Save(usuarioLogado);
        }

        public ArquivoVm BaixarAquivo(int id)
        {
            var processo = _processoRepository.ObterPorId(id);

            var fileName = processo.NumeroSinistro + ".rar";

            var path = Path.Combine(Directories.GetPathUploads(processo.Seguradora.UrlAdministrativo), fileName);

            var fileLength = new FileInfo(path).Length;

            var fileStream = File.Open(path, FileMode.Open, FileAccess.Read);

            return new ArquivoVm(fileName, fileStream, fileLength);
        }
        
        public IEnumerable<ProcessoListVm> SelecionarPorSeguradoraParaDespesas(int? seguradoraId)
        {
            var processos = _processoRepository.SelecionarPorSeguradoraParaDespesas(seguradoraId);

            return processos.Select(p => new ProcessoListVm(p));
        }

        public IEnumerable<ProcessoListVm> SelecionarPorSeguradoraData(int seguradoraId, DateTime dataInicio, DateTime dataFim)
        {
            var processos = _processoRepository.SelecionarPorSeguradoraData(seguradoraId, dataInicio, dataFim);

            return processos.Select(p => new ProcessoListVm(p)).ToList();
        }

        public void Finalizar(ProcessoFinalizarVm processoFinalizar, string usuarioLogado)
        {
            var processo = _processoRepository.ObterPorId(processoFinalizar.Id);

            _processoHistoricoDomainService.FinalizarHistorico(processo);
            processo.FinalizarProcesso(processoFinalizar.NumeroNF, processoFinalizar.DataEmissaoNF);

            _processoRepository.Update(processo);

            Directories.SaveToFolder(processo.Seguradora.UrlFinanceiro, processo.NumeroSinistro.PadLeft(10, '0'), ArquivoTipo.rar, processoFinalizar.Arquivo);

            Save(usuarioLogado);
        }

        public IEnumerable<ProcessoListVm> SelecionarPorSeguradorasAtivas(int seguradoraId)
        {
            var processos = _processoRepository.SelecionarPorSeguradoraCombo(seguradoraId);

            return processos.Select(x => new ProcessoListVm(x));
        }

        public AcionamentoVm BaixarAcionamento(int id)
        {
            var acionamento = _processoRepository.ObterAcionamento(id);


            return new AcionamentoVm(acionamento.Nome, acionamento.Arquivo);
        }

        public AlertaVm ObterAlerta(int usuarioId)
        {
            var perfis = _usuarioRepository.SelecionarPerfis(usuarioId).ToList();

            if (perfis.Select(x => x.Id).Contains((int)PerfilEnum.Financeiro))
            {
                var alerta = _processoRepository.ObterAlertaFinanceiro();

                return new AlertaVm(alerta);
            }
            else
            {
                var alerta = perfis.Select(x => x.Id).Contains((int)PerfilEnum.Analista) ? _processoRepository.ObterAlerta(usuarioId) : _processoRepository.ObterAlerta(null);
                return new AlertaVm(alerta);
            }
        }

        public IEnumerable<SindicateHomeVm> SindicantesProcesosMes()
        {
            var sindicantes = _processoRepository.SelecionarSindicantesStatusMensal();

            return sindicantes.Select(s => new SindicateHomeVm(s)).ToList();
        }


        private Email CriarEmailEnvioSindicanteExterno(Processo processo)
        {
            var assunto = $"Novo Processo para análise - Processo RTF {processo.Id}";
            var de = processo.Sindicantes.FirstOrDefault(x => x.SindicanteTipoId == (int) SindicanteTipoEnum.Interno);
            var para = processo.Sindicantes.FirstOrDefault(x => x.SindicanteTipoId == (int)SindicanteTipoEnum.Externo);
            var corpo = EmailResource.EnviadoSindicanteExterno
                    .Replace("%EMITENTE%", de.DadosPessoaFisica.Nome)
                    .Replace("%DESTINATARIO%", para.DadosPessoaFisica.Nome)
                    .Replace("%NUMEROSINISTRO%", processo.NumeroSinistro)
                    .Replace("%SEGURADO%", processo.Segurado.Nome)
                    .Replace("%SEGURADORA%", processo.Seguradora.DadosPessoaJuridica.RazaoSocial);
            var anexo = processo.Acionamento?.Arquivo;

            return new Email(assunto, para.Email, de.Email, corpo, anexo);
        }
    }
}
