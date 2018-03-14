using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Services;
using Application.Financeiro.Contracts;
using Application.Financeiro.ViewModels.Lancamentos;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Core.UnitOfWork;

namespace Application.Financeiro.Services
{
    public class LancamentoAppService :ApplicationService, ILancamentoAppService
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IReciboRepository _reciboRepository;
        private readonly IReciboSindicanteRepository _reciboSindicanteRepository;

        public LancamentoAppService(IUnitOfWork unitOfWork, ILancamentoRepository lancamentoRepository, IReciboRepository reciboRepository, IReciboSindicanteRepository reciboSindicanteRepository)
            :base(unitOfWork)
        {
            _lancamentoRepository = lancamentoRepository;
            _reciboRepository = reciboRepository;
            _reciboSindicanteRepository = reciboSindicanteRepository;
        }

        public ReciboVm ObterRecibo(int sindicanteId, int processoId)
        {            
            var recibo = _reciboRepository.ObterRecibo(sindicanteId, processoId);

            return new ReciboVm(recibo);
        }

        public void Registrar(LancamentoRegistrarVm model, string usuarioLogado)
        {
            var lancamento = new Lancamento(model.ReciboId, model.LancamentoTipo,model.Descricao,model.Valor,model.TipoFinanceiro, model.Observacao);

            _lancamentoRepository.Add(lancamento);
            Save(usuarioLogado);
        }

        public void Excluir(int id, string usuarioLogado)
        {
            var lancamento = _lancamentoRepository.ObterPorId(id);

            _lancamentoRepository.Delete(lancamento);
            Save(usuarioLogado);
        }

        public void FecharLancamento(int id, string usuarioLogado)
        {
            var recibo = _reciboRepository.ObterId(id);
            recibo.Fechar();

            _reciboRepository.Update(recibo);
            Save(usuarioLogado);
        }

        public void ReabrirLancamento(int id, string usuarioLogado)
        {
            var recibo = _reciboRepository.ObterId(id);
            recibo.Abrir();

            _reciboRepository.Update(recibo);
            Save(usuarioLogado);
        }

        public void FecharPagamento(List<int> recibosIds, DateTime dataInicio, DateTime dataFim, string usuarioLogado)
        {
            var recibos = _reciboRepository.SelecionarPorIds(recibosIds).ToList();

            foreach (var recibo in recibos)
            {
                recibo.Emitir();
                _reciboRepository.Update(recibo);
            }

            var sindicante = recibos.Select(x=>x.Sindicante).FirstOrDefault();

            var reciboSindicante = new ReciboSindicante(sindicante, dataInicio, dataFim, recibos);
            _reciboSindicanteRepository.Add(reciboSindicante);

            Save(usuarioLogado);
        }

        public IEnumerable<ReciboPagamentoListVm> SelecionarPorSindicanteData(int sindicanteId, DateTime dataInicio, DateTime dataFim)
        {
            var recibos = _lancamentoRepository.SelecionarPorSindicanteData(sindicanteId, dataInicio, dataFim);

            return recibos.Select(r => new ReciboPagamentoListVm(r)).ToList();
        }
    }
}
