using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Domain.Core.Entities;
using SharedKernel.Enums;

namespace Domain.Financeiro.Entities.RecibosSindicantes
{
    public class Recibo: EntityId
    {
        public int ProcessoId { get; private set; }
        public int SindicanteId { get; private set; }
        public ICollection<Lancamento> Lancamentos { get; private set; }
        public StatusRecibo Status { get; private set; }

        public Processo Processo { get; set; }
        public Sindicante Sindicante { get; set; }

        protected Recibo()
        {
            
        }

        public Recibo(int processoId, int sindicanteId, ICollection<Lancamento> lancamentos)
        {
            ProcessoId = processoId;
            SindicanteId = sindicanteId;
            Lancamentos = lancamentos;
            Status = StatusRecibo.Aberto;
        }

        public Recibo(Processo processo, int sindicanteId, ICollection<Lancamento> lancamentos)
        {
            Processo = processo;
            SindicanteId = sindicanteId;
            Lancamentos = lancamentos;
            Status = StatusRecibo.Aberto;
        }

        public void IncluirLancamento(List<Lancamento> lancamentos)
        {
            foreach (var lancamento in lancamentos)
            {
                Lancamentos.Add(lancamento);
            }
        }

        public void Fechar()
        {
            Status = StatusRecibo.Fechado;
        }

        public void Abrir()
        {
            Status = StatusRecibo.Aberto;
        }

        public void Emitir()
        {
           Status = StatusRecibo.Emitido;
        }
    }
}
