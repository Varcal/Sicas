using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Application.Core.ViewModels;
using Domain.Financeiro.Entities.DespesasSeguradora;
using SharedKernel.Enums;

namespace Application.Financeiro.ViewModels.Despesas
{
    public class DespesaVm
    {
        public int Id { get; set; }
        public int ProcessoId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnderecoVm Origem { get; set; }
        public EnderecoVm Destino { get; set; }
        public decimal Kms { get; set; }
        public decimal ValorKm { get; set; }
        public decimal PedagioTransporte { get; set; }
        public decimal EstadiaAlimentacao { get; set; }
        public decimal Total { get; set; }
        public ICollection<DespesaAdicionalVm> DespesasAdicionais { get; set; }


        public DespesaVm()
        {
            
        }

        public DespesaVm(int processoId, string descricao, DateTime data, EnderecoVm origem, EnderecoVm destino, decimal kms, decimal valorKm, [Optional]ICollection<DespesaAdicionalVm> despesasAdicionais)
        {
            ProcessoId = processoId;
            Descricao = descricao;
            Data = data;
            Origem = origem;
            Destino = destino;
            Kms = kms;
            ValorKm = valorKm;
            CalcularDespesasAdicionais(despesasAdicionais);
            DespesasAdicionais = despesasAdicionais;
            Total = PedagioTransporte + EstadiaAlimentacao + ValorKm;
        }

        private void CalcularDespesasAdicionais(ICollection<DespesaAdicionalVm> despesasAdicionais)
        {
            foreach (var despesaAdicional in despesasAdicionais)
            {
                if (despesaAdicional.DespesaTipoId == (int)DespesaTipoEnum.Pedagio || despesaAdicional.DespesaTipoId == (int)DespesaTipoEnum.Transporte)
                    PedagioTransporte += despesaAdicional.Valor;

                if (despesaAdicional.DespesaTipoId == (int)DespesaTipoEnum.Refeicao || despesaAdicional.DespesaTipoId == (int)DespesaTipoEnum.Hospedagem)
                    EstadiaAlimentacao += despesaAdicional.Valor;
            }
        }

        public DespesaVm(int id, int processoId, string descricao, DateTime data, EnderecoVm origem, EnderecoVm destino, decimal kms, decimal valorKm, [Optional]ICollection<DespesaAdicionalVm> despesasAdicionais)
            :this(processoId, descricao, data, origem, destino, kms, valorKm, despesasAdicionais)
        {
            Id = id;
        }

        public DespesaVm(Despesa despesa, List<DespesaAdicionalVm> despesasAdicionais)
        {
            Id = despesa.Id;
            ProcessoId = despesa.ProcessoId;
            Descricao = despesa.Descricao;
            Data = despesa.Data;
            Origem = new EnderecoVm(despesa.Origem);
            Destino = new EnderecoVm(despesa.Destino);
            Kms = despesa.Kms;
            ValorKm = despesa.ValorKm;
            CalcularDespesasAdicionais(despesasAdicionais);
            DespesasAdicionais = despesasAdicionais;
            Total = PedagioTransporte + EstadiaAlimentacao + ValorKm;
        }
    }
}
