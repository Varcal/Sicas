using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.EventosTipos
{
    public class EventoTipoVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public EventoTipoVm()
        {
            
        }

        public EventoTipoVm(EventoTipo entity)
        {
            Id = entity.Id;
            Nome = entity.Nome;
        }
    }
}
