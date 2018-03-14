using Domain.Administrativo.Scopes;
using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class Evento: EntityBase
    {
        public int SeguradoraId { get; set; }
        public int ServicoSeguradoraId { get; set; }
        public int EventoTipoId { get; set; }      
        public int FranquiaKm { get; set; }
        public decimal Honorario { get; set; }


        public ServicoSeguradora ServicoSeguradora { get; private set; }
        public EventoTipo EventoTipo { get; private set; }

        #region Construtores
        protected Evento()
        {

        }

        public Evento(int servicoSeguradoraId, int eventoTipoId, int franquiaKm, decimal honorario)
        {
            ServicoSeguradoraId = servicoSeguradoraId;
            EventoTipoId = eventoTipoId;
            FranquiaKm = franquiaKm;
            Honorario = honorario;
        }

        public bool Alterar(Evento evento)
        {
            if (SeguradoraId != evento.SeguradoraId || ServicoSeguradoraId != evento.ServicoSeguradoraId ||
                EventoTipoId != evento.EventoTipoId) return false;

            FranquiaKm = evento.FranquiaKm;
            Honorario = evento.Honorario;
            return true;
        }

        #endregion

        public override bool IsValid()
        {
            return this.CriarEventoScopeSeValido();
        }
    }
}
