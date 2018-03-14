using System;
using System.Linq;
using Domain.Core.Entities;
using SharedKernel.Enums;

namespace Domain.Financeiro.Entities.RecibosSindicantes
{
    public class ReciboSindicanteItem: EntityId
    {
        public int ReciboSindicanteId { get; set; }
        public string Segurado { get; private set; }
        public string Sinistro { get; private set; }
        public string Seguradora { get; private set; }
        public decimal Despesa { get; private set; }
        public decimal Honorario { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal OutrosCreditos { get; private set; }
        public DateTime DataTerminoTrabalho { get; private set; }

        protected ReciboSindicanteItem()
        {

        }

        public ReciboSindicanteItem(Recibo recibo)
        {
            Segurado = recibo.Processo.Segurado.Nome;
            Sinistro = recibo.Processo.NumeroSinistro;
            Seguradora = recibo.Processo.Seguradora.DadosPessoaJuridica.RazaoSocial;
            Despesa = recibo.Lancamentos.Where(x => x.LancamentoTipo == LancamentoTipo.Despesa).Sum(x => x.Valor);
            Honorario = recibo.Lancamentos.Where(x => x.LancamentoTipo == LancamentoTipo.Honorario).Sum(x => x.Valor);
            OutrosCreditos = recibo.Lancamentos.Where(x => x.LancamentoTipo == LancamentoTipo.Outros && x.TipoFinanceiro == TipoFinanceiro.Credito).Sum(x => x.Valor);
            Desconto = recibo.Lancamentos.Where(x => x.LancamentoTipo == LancamentoTipo.Vale || (x.LancamentoTipo == LancamentoTipo.Outros && x.TipoFinanceiro == TipoFinanceiro.Debito)).Sum(x => x.Valor);
            DataTerminoTrabalho = recibo.Processo.FinalizadaAnaliseEm.Value;
        }
    }
}