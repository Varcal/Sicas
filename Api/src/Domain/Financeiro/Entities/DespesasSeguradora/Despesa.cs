using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Entities;
using Domain.Financeiro.Scopes;

namespace Domain.Financeiro.Entities.DespesasSeguradora
{
    public class Despesa: EntityId
    {
        public int ProcessoId { get; private set; }
        public string Descricao { get; private set; }
        public DateTime Data { get; private set; }
        public int EnderecoOrigemId { get; private set; }
        public int EnderecoDestinoId { get; private set; }
        public EnderecoDespesa Origem { get; private set; }
        public EnderecoDespesa Destino { get; private set; }
        public decimal Kms { get; private set; }
        public decimal ValorKm { get; private set; }
        public ICollection<DespesaAdicional> DespesasAdicionais { get; private set; }


        protected Despesa()
        {
            
        }

        public Despesa(int processoId, string descricao, DateTime data, EnderecoDespesa origem, EnderecoDespesa destino, decimal kms, decimal valorKm, ICollection<DespesaAdicional> despesasAdicionais)
        {
            ProcessoId = processoId;
            Descricao = descricao;
            Data = data;
            Origem = origem;
            Destino = destino;
            Kms = kms;
            ValorKm = valorKm;
            DespesasAdicionais = despesasAdicionais;

            IsValid();
        }

        public void Alterar(string descricao, DateTime data, EnderecoDespesa origem, EnderecoDespesa destino, decimal kms, decimal valorKm, IEnumerable<DespesaAdicional> despesasAdicionais)
        {
            Descricao = descricao;
            Data = data;
            Origem.Alterar(origem);
            Destino.Alterar(destino);
            Kms = kms;
            ValorKm = valorKm;
            DespesasAdicionais = despesasAdicionais.ToList();
        }

        public sealed override bool IsValid()
        {
            return this.CriarSeEscopoValido();
        }
    }
}
