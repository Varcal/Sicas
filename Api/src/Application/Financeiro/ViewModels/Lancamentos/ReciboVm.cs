using System.Collections.Generic;
using System.Linq;
using Domain.Financeiro.Entities.RecibosSindicantes;
using SharedKernel.Enums;

namespace Application.Financeiro.ViewModels.Lancamentos
{
    public class ReciboVm
    {
        public int Id { get; set; }
        public StatusRecibo Status { get; set; }
        public ICollection<LancamentoListVm> Lancamentos { get; set; }


        public ReciboVm()
        {
            
        }

        public ReciboVm(Recibo recibo)
        {
            Id = recibo.Id;
            Status = recibo.Status;
            Lancamentos = recibo.Lancamentos.Select(l => new LancamentoListVm(l)).ToList();
        }
    }
}
