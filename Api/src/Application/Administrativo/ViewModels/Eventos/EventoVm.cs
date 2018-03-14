using Application.Administrativo.ViewModels.EventosTipos;
using Application.Administrativo.ViewModels.ServicosSeguradoras;
using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Eventos
{
    public class EventoVm
    {
        public int SeguradoraId { get; set; }
        public int ServicoSeguradoraId { get; set; }
        public int EventoTipoId { get; set; }
        public int FranquiaKm { get; set; }
        public decimal Honorario { get; set; }
        public EventoTipoVm EventoTipo { get; set; }
        public ServicoSeguradoraVm ServicoSeguradora { get; set; }



        public EventoVm()
        {
            
        }

        public EventoVm(Evento evento)
        {
            SeguradoraId = evento.SeguradoraId;
            ServicoSeguradoraId = evento.ServicoSeguradoraId;
            EventoTipoId = evento.EventoTipoId;
            FranquiaKm = evento.FranquiaKm;
            Honorario = evento.Honorario;
            EventoTipo = evento.EventoTipo != null ? new EventoTipoVm(evento.EventoTipo): null;
            ServicoSeguradora = evento.ServicoSeguradora != null ? new ServicoSeguradoraVm(evento.ServicoSeguradora): null;
        }
    }
}