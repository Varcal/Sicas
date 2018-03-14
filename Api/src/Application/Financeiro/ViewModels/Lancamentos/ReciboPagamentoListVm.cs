using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Financeiro.Entities.RecibosSindicantes;
using SharedKernel.Enums;

namespace Application.Financeiro.ViewModels.Lancamentos
{
    public class ReciboPagamentoListVm
    {
        public int Id { get;  set; }
        public string NumeroSinistro { get;  set; }
        public string Seguradora { get;  set; }
        public StatusRecibo StatusId { get; set; }
        public string Status { get; private set; }
        public DateTime Conclusao { get; set; }
        public decimal Total { get;  set; }
        public IEnumerable<LancamentoListVm> Lancamentos { get; set; }

        public ReciboPagamentoListVm()
        {
            
        }

        public ReciboPagamentoListVm(Recibo recibo)
        {
            Id = recibo.Id;
            NumeroSinistro = recibo.Processo.NumeroSinistro;
            Seguradora = recibo.Processo.Seguradora.DadosPessoaJuridica.RazaoSocial;
            StatusId = recibo.Status;
            Status = recibo.Status.ToString();
            Conclusao = recibo.Processo.FinalizadaAnaliseEm.Value;
            Total = recibo.Lancamentos.Where(x=>x.TipoFinanceiro == TipoFinanceiro.Credito).Sum(x=>x.Valor) - recibo.Lancamentos.Where(x=>x.TipoFinanceiro == TipoFinanceiro.Debito).Sum(x=>x.Valor);
            Lancamentos = recibo.Lancamentos.Select(l => new LancamentoListVm(l)).ToList();
        }
    }
}
