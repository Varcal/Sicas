using System.Collections.Generic;
using System.Linq;
using Domain.Administrativo.Entities;

namespace Application.Financeiro.ViewModels.Despesas
{
    public class ProcessoDespesaVm
    {
        public int Id { get; set; }
        public string NumeroSinistro { get; set; }
        public string NumeroReferencia { get; set; }
        public string Seguradora { get; set; }
        public decimal ValorKm { get; set; }
       public ICollection<DespesaVm> Despesas { get; set; }

        public ProcessoDespesaVm()
        {
            Despesas = new List<DespesaVm>();
        }


        public ProcessoDespesaVm(int id, string numeroSinistro, string numeroReferencia, string seguradora, decimal valorKm, ICollection<DespesaVm> despesaVms)
        {
            Id = id;
            NumeroSinistro = numeroSinistro;
            NumeroReferencia = numeroReferencia;
            Seguradora = seguradora;
            ValorKm = valorKm;
            Despesas = despesaVms;
        }

        public ProcessoDespesaVm(Processo processo)
        {
            Id = processo.Id;
            NumeroSinistro = processo.NumeroSinistro;
            NumeroReferencia = processo.NumeroReferencia;
            Seguradora = processo.Seguradora.DadosPessoaJuridica.RazaoSocial;
            ValorKm = processo.Seguradora.ValorPorKm;

            var despesas = (
                            from despesa in processo.Despesas
                                let despesasAdicionais = despesa.DespesasAdicionais.Select(despesasAdicional => new DespesaAdicionalVm(despesasAdicional)).ToList()
                            select new DespesaVm(despesa, despesasAdicionais)
                            ).ToList();

            Despesas = despesas;
        }
    }
}
