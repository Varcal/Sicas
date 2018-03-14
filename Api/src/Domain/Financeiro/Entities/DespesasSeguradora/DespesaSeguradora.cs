using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Entities;
using Domain.Core.Entities;

namespace Domain.Financeiro.Entities.DespesasSeguradora
{
    public class DespesaSeguradora: EntityId
    {
        public int ProcessoId { get; set; }
        public string Cia { get; private set; }
        public string Sinistro { get; private set; }
        public string Segurado { get; private set; }
        public string Analista { get; private set; }
        public DateTime DataConclusao { get; private set; }
        public decimal ValorKm { get; private set; }
        public Processo Processo { get; set; }
        public ICollection<DespesaSeguradoraItem> DespesasSeguradoraItems { get; private set; }


        protected DespesaSeguradora()
        {
            
        }

        public DespesaSeguradora(Processo processo)
        {
            Processo = processo;
            Cia = processo.Seguradora.DadosPessoaJuridica.RazaoSocial;
            Sinistro = processo.NumeroSinistro;
            Segurado = processo.Segurado.Nome;
            Analista = processo.Analista;
            DataConclusao = processo.EmitidoParecerEm.Value;
            ValorKm = processo.Seguradora.ValorPorKm;
            DespesasSeguradoraItems = processo.Despesas.Select(d => new DespesaSeguradoraItem(d)).ToList();
        }
    }
}
