using System.Collections.Generic;

namespace Application.Financeiro.ViewModels.Despesas
{
    public class ProcessoDespesaRegistrarVm
    {
        public int Id { get; set; }
        public IList<DespesaVm> Despesas { get; set; }

        public ProcessoDespesaRegistrarVm()
        {
            
        }

        public ProcessoDespesaRegistrarVm(int id, IList<DespesaVm> despesas)
        {
            Id = id;
            Despesas = despesas;
        }
    }
}
