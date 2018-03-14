using Domain.Financeiro.Entities.DespesasSeguradora;
using SharedKernel.Enums;
using SharedKernel.Helpers;

namespace Application.Financeiro.ViewModels.Despesas
{
    public class DespesaAdicionalVm
    {
        public int Id { get; set; }
        public int DespesaTipoId { get; set; }
        public string Descricao { get; set; }
        public int DespesaId { get; set; }
        public decimal Valor { get; set; }


        public DespesaAdicionalVm()
        {
            
        }

        public DespesaAdicionalVm(int despesaTipoId, int despesaId, decimal valor)
        {
            DespesaTipoId = despesaTipoId;
            DespesaId = despesaId;
            Valor = valor;
        }

        public DespesaAdicionalVm(int id, int despesaTipoId, int despesaId, decimal valor)
            :this(despesaTipoId, despesaId, valor)
        {
            Id = id;
        }

        public DespesaAdicionalVm(DespesaAdicional despesaAdicional)
        {
            Id = despesaAdicional.Id;
            DespesaTipoId = despesaAdicional.DespesaTipoId;
            Descricao = Helper.GetDescription((DespesaTipoEnum)despesaAdicional.DespesaTipoId);
            DespesaId = despesaAdicional.DespesaId;
            Valor = despesaAdicional.Valor;
        }
    }
}
