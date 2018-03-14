using Application.Administrativo.ViewModels.EventosTipos;
using Application.Administrativo.ViewModels.ServicosSeguradoras;
using Application.Administrativo.ViewModels.ServicosSindicantes;
using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.Sindicantes
{
    public class HonorarioVm
    {
        public int SindicanteId { get; private set; }
        public int ServicoSeguradoraId { get; private set; }
        public int EventoTipoId { get; private set; }
        public int ServicoSindicanteId { get; private set; }
        public decimal Valor { get; private set; }
        public ServicoSeguradoraVm ServicoSeguradora { get; set; }
        public EventoTipoVm EventoTipo { get; set; }
        public ServicoSindicanteVm ServicoSindicante { get; set; }

        public HonorarioVm()
        {
            
        }

        public HonorarioVm(int servicoSeguradoraId, int eventoTipoId, int servicoSindicanteId, decimal valor)
        {
            ServicoSeguradoraId = servicoSeguradoraId;
            EventoTipoId = eventoTipoId;
            ServicoSindicanteId = servicoSindicanteId;
            Valor = valor;
        }

        public HonorarioVm(Honorario honorario)
        {
            ServicoSeguradoraId = honorario.ServicoSeguradora.Id;
            EventoTipoId = honorario.EventoTipo.Id;
            ServicoSindicanteId = honorario.ServicoSindicante.Id;
            Valor = honorario.Valor;
            ServicoSeguradora = new ServicoSeguradoraVm(honorario.ServicoSeguradora);
            EventoTipo = new EventoTipoVm(honorario.EventoTipo);
            ServicoSindicante = new ServicoSindicanteVm(honorario.ServicoSindicante);
        }


        public Honorario ToHonorario(HonorarioVm model)
        {
            return new Honorario(model.ServicoSeguradoraId, model.EventoTipoId, model.ServicoSindicanteId, model.Valor);
        }
    }
}
