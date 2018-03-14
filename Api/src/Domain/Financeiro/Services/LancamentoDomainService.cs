using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Entities;
using Domain.Financeiro.Contracts.Repositories;
using Domain.Financeiro.Contracts.Services;
using Domain.Financeiro.Entities.RecibosSindicantes;
using SharedKernel.Enums;
using SharedKernel.Helpers;

namespace Domain.Financeiro.Services
{
    public class LancamentoDomainService : ILancamentoDomainService
    {
        private readonly IReciboRepository _reciboRepository;

        public LancamentoDomainService(IReciboRepository reciboRepository)
        {
            _reciboRepository = reciboRepository;
        }

        public void EfetuarLancamentosDeDespesaAutomatico(Processo processo)
        {
            var sindicante = processo.Sindicantes.SingleOrDefault(x => x.SindicanteTipoId == (int)SindicanteTipoEnum.Externo);

            if (sindicante == null) return;

            var lancamentos = GerarLancamentosDespesa(processo, sindicante).ToList();

            var recibo = _reciboRepository.ObterRecibo(sindicante.Id, processo.Id);

            recibo.IncluirLancamento(lancamentos);

            _reciboRepository.Update(recibo);
        }

        public void GerarLancamentoHonorario(Processo processo, IList<Sindicante> sindicantes)
        {
            foreach (var sindicante in sindicantes)
            {
                GerarLancamentoHonorario(processo, sindicante);
            }
        }


        #region Métodos Auxiliares

        private void GerarLancamentoHonorario(Processo processo, Sindicante sindicante)
        {
            var lancamentos = new List<Lancamento>();

            var honorario = sindicante.Honorarios.SingleOrDefault(x => x.ServicoSindicanteId == processo.ServicoSindicanteId && x.ServicoSeguradoraId == processo.ServicoSeguradoraId && processo.EventoTipoId == x.EventoTipoId);

            if (honorario != null)
            {
                var lancamentoHonorario = new Lancamento(LancamentoTipo.Honorario, LancamentoTipo.Honorario.GetDescription(),
                honorario.Valor, TipoFinanceiro.Credito);
                lancamentos.Add(lancamentoHonorario);
            }

            var recibo = _reciboRepository.ObterRecibo(sindicante.Id, processo.Id);

            if (recibo != null)
            {
                recibo.IncluirLancamento(lancamentos);
                _reciboRepository.Update(recibo);
            }
            else
            {
                var reciboNovo = new Recibo(processo, sindicante.Id, lancamentos);
                _reciboRepository.Add(reciboNovo);
            }
        }

        private IEnumerable<Lancamento> GerarLancamentosDespesa(Processo processo, Sindicante sindicante)
        {
            var lancamentos = new List<Lancamento>();


            if (sindicante != null && sindicante.SindicanteTipo.Id == (int)SindicanteTipoEnum.Externo)
            {
                foreach (var despesa in processo.Despesas)
                {
                    var valor = despesa.DespesasAdicionais.Sum(da => da.Valor) + despesa.ValorKm;

                    var lacamentoDepesa = new LancamentoDespesa(despesa.Id, LancamentoTipo.Despesa, LancamentoTipo.Despesa.ToString(), valor, TipoFinanceiro.Credito);
                    lancamentos.Add(lacamentoDepesa);
                }

            }

            return lancamentos;
        }

        #endregion    
    }
}
