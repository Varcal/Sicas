using System;
using System.Linq;
using Domain.Core.Entities;
using SharedKernel.Enums;

namespace Domain.Administrativo.Entities
{
    public class ProcessoHistorico: EntityId
    {
        public int SeguradoraId { get; set; }
        public int ProcessoId { get; private set; }
        public int SindicanteId { get; private set; }
        public int StatusId { get; set; }
        public DateTime IniciadoEm { get; private set; }
        public DateTime? FinalizadoEm { get; private set; }

        public Seguradora Seguradora { get; private set; }
        public Sindicante Sindicante { get; private set; }
        public Status Status { get; private set; }

        public ProcessoHistorico()
        {
            
        }

        public ProcessoHistorico(Processo processo)
        {
            SeguradoraId = processo.SeguradoraId;
            SindicanteId = processo.Sindicantes.First().Id;
            ProcessoId = processo.Id;
            StatusId = processo.StatusId;
            IniciadoEm = processo.StatusId == 1 ? processo.DataCadastro : DateTime.Now;
            FinalizadoEm = processo.FinalizadoEm;
        }

        public void FinalizarHistorico()
        {
            FinalizadoEm = DateTime.Now;
        }


        public void Cancelar()
        {
            FinalizadoEm = DateTime.Now;
            StatusId = (int)StatusEnum.Cancelado;
        }
    }
}
