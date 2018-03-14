using System;
using Domain.Financeiro.Entities.DespesasSeguradora;

namespace CrossCutting.Reports.DespesasSeguradora
{
    public class DespesaSeguradoraItemDto
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public decimal KmPercorrido { get; set; }
        public decimal ValorKmPercorrido { get; set; }
        public decimal TransportePedagio { get; set; }
        public decimal HospedagemRefeicao { get; set; }
        public decimal Total { get; set; }

        public DespesaSeguradoraItemDto(DespesaSeguradoraItem despesaSeguradoraItem)
        {
            Data = despesaSeguradoraItem.Data;
            Descricao = despesaSeguradoraItem.Descricao;
            Endereco = despesaSeguradoraItem.Endereco;
            KmPercorrido = despesaSeguradoraItem.KmPercorrido;
            ValorKmPercorrido = despesaSeguradoraItem.ValorKmPercorrido;
            TransportePedagio = despesaSeguradoraItem.TransportePedagio;
            HospedagemRefeicao = despesaSeguradoraItem.HospedagemRefeicao;
            Total = despesaSeguradoraItem.Total;
        }
    }
}