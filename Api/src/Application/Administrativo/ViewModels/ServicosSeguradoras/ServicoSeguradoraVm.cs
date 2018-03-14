using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.ViewModels.EventosTipos;
using Domain.Administrativo.Entities;

namespace Application.Administrativo.ViewModels.ServicosSeguradoras
{
    public class ServicoSeguradoraVm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<EventoTipoVm> EventosTipos { get; set; }

        public ServicoSeguradoraVm()
        {

        }

        public ServicoSeguradoraVm(string nome, List<EventoTipoVm> eventosTipos)
        {
            Nome = nome;
            EventosTipos = eventosTipos;
        }

        public ServicoSeguradoraVm(int id, string nome, List<EventoTipoVm> eventosTipos)
            :this(nome, eventosTipos)
        {
            Id = id;
        }

        public ServicoSeguradora ToServicoSeguradora(ServicoSeguradoraVm model, IList<EventoTipo> eventoTipoList)
        {
            return new ServicoSeguradora(model.Nome, eventoTipoList);
        }

        public ServicoSeguradoraVm(ServicoSeguradora servicoSeguradora)
        {
            Id = servicoSeguradora.Id;
            Nome = servicoSeguradora.Nome;
            EventosTipos = servicoSeguradora.EventoTipos != null ? servicoSeguradora.EventoTipos.Select(eventoTipo => new EventoTipoVm(eventoTipo)).ToList(): null;
        }
    }

}
