using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class Honorario: EntityBase
    {
        public int SindicanteId { get; private set; }
        public int ServicoSeguradoraId { get; private set; }
        public int EventoTipoId { get; private set; }
        public int ServicoSindicanteId { get; private set; }     
        public decimal Valor { get; private set; }


        public ServicoSeguradora ServicoSeguradora { get; private set; }
        public ServicoSindicante ServicoSindicante { get; private set; }
        public EventoTipo EventoTipo { get; private set; }


        public Honorario()
        {
            
        }

        public Honorario(int servicoSeguradoraId, int eventoTipoId, int servicoSindicanteId, decimal valor)
        {
            ServicoSeguradoraId = servicoSeguradoraId;
            EventoTipoId = eventoTipoId;
            ServicoSindicanteId = servicoSindicanteId;
            Valor = valor;
        }

        public Honorario(int servicoSeguradoraId, int eventoTipoId, int servicoSindicanteId, int sindicanteId, decimal valor)
            :this(servicoSeguradoraId,eventoTipoId, servicoSindicanteId, valor)
        {           
            SindicanteId = sindicanteId;
        }


        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}