using System.Collections.Generic;
using System.Linq;
using Application.Core.Services;
using Application.Financeiro.Contracts;
using Application.Financeiro.ViewModels;
using Application.Financeiro.ViewModels.Despesas;
using CrossCutting.Reports.ReportsGenerator.Interfaces;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Contracts.Services;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Contracts.Services;
using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.UnitOfWork;
using SharedKernel.Enums;

namespace Application.Financeiro.Services
{
    public class DespesaAppService:ApplicationService, IDespesaAppService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IEnderecoDespesaDomainService _enderecoDespesaDomainService;
        private readonly IDespesaRepository _despesaRepository;
        private readonly IDespesaSeguradoraGeradorRelatorio _despesaSeguradoraGeradorRelatorio;
        private readonly IProcessoHistoricoDomainService _processoHistoricoDomainService;
        private readonly ILancamentoDomainService _lancamentoDomainService;
        private readonly IDespesaSeguradoraRepository _despesaSeguradoraRepository;
        private readonly IEventoRepository _eventoRepository;

        public DespesaAppService(IUnitOfWork uow, IProcessoRepository processoRepository, IEnderecoDespesaDomainService enderecoDespesaDomainService, IDespesaRepository despesaRepository, IDespesaSeguradoraGeradorRelatorio despesaSeguradoraGeradorRelatorio, IProcessoHistoricoDomainService processoHistoricoDomainService, ILancamentoDomainService lancamentoDomainService, IDespesaSeguradoraRepository despesaSeguradoraRepository, IEventoRepository eventoRepository) 
            : base(uow)
        {
            _processoRepository = processoRepository;
            _enderecoDespesaDomainService = enderecoDespesaDomainService;
            _despesaRepository = despesaRepository;
            _despesaSeguradoraGeradorRelatorio = despesaSeguradoraGeradorRelatorio;
            _processoHistoricoDomainService = processoHistoricoDomainService;
            _lancamentoDomainService = lancamentoDomainService;
            _despesaSeguradoraRepository = despesaSeguradoraRepository;
            _eventoRepository = eventoRepository;
        }


        public void Registrar(ProcessoDespesaRegistrarVm model, string usuarioLogado)
        {

            IList<Despesa> despesas = (
                from despesaVm in model.Despesas.Where(x=>x.Id == 0)
                    let despesasAdicionais = despesaVm.DespesasAdicionais.Select(da => new DespesaAdicional(da.DespesaTipoId, da.Valor)).ToList()
                    let partida = _enderecoDespesaDomainService.RetornEnderecoDespesa(despesaVm.Origem.Logradouro, despesaVm.Origem.Numero, despesaVm.Origem.Cep, despesaVm.Origem.Bairro, despesaVm.Origem.CidadeId, despesaVm.Origem.Complemento)
                    let chegada = _enderecoDespesaDomainService.RetornEnderecoDespesa(despesaVm.Destino.Logradouro, despesaVm.Destino.Numero, despesaVm.Destino.Cep, despesaVm.Destino.Bairro, despesaVm.Destino.CidadeId, despesaVm.Destino.Complemento)
                select new Despesa(despesaVm.ProcessoId, despesaVm.Descricao, despesaVm.Data, partida, chegada, despesaVm.Kms, despesaVm.ValorKm, despesasAdicionais))
                .ToList();

            var processo = _processoRepository.ObterPorId(model.Id);

            var franquiaKm = _eventoRepository.ObterFranquiaKm(processo.SeguradoraId, processo.ServicoSeguradoraId, processo.EventoTipoId);

            processo.AdicionarDespesas(despesas);

            _processoRepository.Update(processo);

            Save(usuarioLogado);
        }

        public ProcessoDespesaVm ObterPorId(int id)
        {
            var processo = _processoRepository.ObterParaDespesa(id);

            return new ProcessoDespesaVm(processo);
        }

        public void AlterarDespesa(DespesaVm despesaVm, string usuarioLogado)
        {
            var origem = _enderecoDespesaDomainService.RetornEnderecoDespesa(despesaVm.Origem.Logradouro, despesaVm.Origem.Numero, despesaVm.Origem.Cep, despesaVm.Origem.Bairro, despesaVm.Origem.CidadeId, despesaVm.Origem.Complemento);
            var destino = _enderecoDespesaDomainService.RetornEnderecoDespesa(despesaVm.Destino.Logradouro, despesaVm.Destino.Numero, despesaVm.Destino.Cep, despesaVm.Destino.Bairro, despesaVm.Destino.CidadeId, despesaVm.Destino.Complemento);
            var despesasAdicionais = despesaVm.DespesasAdicionais.Select(da => new DespesaAdicional(da.Id, da.DespesaId, da.DespesaTipoId, da.Valor)).ToList();

            var despesa = _despesaRepository.GetById(despesaVm.Id);       
                    
            _despesaRepository.ExcluirDespesasAdicionais(despesa.DespesasAdicionais);
        
            despesa.Alterar(despesaVm.Descricao, despesaVm.Data, origem, destino, despesaVm.Kms, despesaVm.ValorKm, despesasAdicionais);

            _despesaRepository.Update(despesa);
            Save(usuarioLogado);

            
        }

        public void Excluir(int id, string usuarioLogado)
        {
            var despesa = _despesaRepository.GetById(id);
            _despesaRepository.Delete(despesa);
            Save(usuarioLogado);
        }

        public ReciboReportVm EmitirRecibo(int processoId, string usuarioLogado)
        {
            var processo = _processoRepository.SelecionarProcessoParaRecibo(processoId);

            if (processo.StatusId == (int) StatusEnum.AguardandoEmissaoNf)
            {
                _processoHistoricoDomainService.FinalizarHistorico(processo);
                processo.EmitirNf();
                _processoRepository.Update(processo);
                _lancamentoDomainService.EfetuarLancamentosDeDespesaAutomatico(processo);
                Save(usuarioLogado);
            }

            var desepesaSeguradora = _despesaSeguradoraRepository.SelecionarPorProcesso(processoId);

            var report = _despesaSeguradoraGeradorRelatorio.GerarRelatorio(desepesaSeguradora);

            return new ReciboReportVm(processo.NumeroSinistro, report.Bytes);
        }

        
    }
}
