using System;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace CrossCutting.Reports.RecibosSindicantes
{
    public class LancamentoDto
    {
        public string Segurado { get; set; }
        public string Sinistro { get; set; }
        public string Cia { get; set; }
        public decimal Despesa { get; set; }
        public decimal Honorario { get; set; }
        public decimal Desconto { get; set; }
        public decimal OutrosCreditos { get; set; }
        public DateTime DataTerminoTrabalho { get; set; }

        protected LancamentoDto()
        {

        }

        public LancamentoDto(ReciboSindicanteItem recibo)
        {
            Segurado = recibo.Segurado;
            Sinistro = recibo.Sinistro;
            Cia = recibo.Seguradora;
            Despesa = recibo.Despesa;
            Honorario = recibo.Honorario;
            OutrosCreditos = recibo.OutrosCreditos;
            Desconto = recibo.Desconto;
            DataTerminoTrabalho = recibo.DataTerminoTrabalho;
        }
    }
}