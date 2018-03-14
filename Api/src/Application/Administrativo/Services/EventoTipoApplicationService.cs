using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.EventosTipos;
using Application.Core.Services;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class EventoTipoApplicationService: ApplicationService, IEventoTipoApplicationService
    {
        private readonly IEventoTipoRepository _eventoTipoRepository;

        public EventoTipoApplicationService(IUnitOfWork uow, IEventoTipoRepository eventoTipoRepository) : base(uow)
        {
            _eventoTipoRepository = eventoTipoRepository;
        }

        public void Registrar(EventoTipoVm model, string usuarioLogado)
        {
            var eventoTipo = new EventoTipo(model.Nome);

            _eventoTipoRepository.Add(eventoTipo);

            Save(usuarioLogado);
        }

        public void Excluir(int id, string usuarioLogado)
        {
            var eventoTipo = _eventoTipoRepository.ObterPorId(id);
            _eventoTipoRepository.Delete(eventoTipo);

            Save(usuarioLogado);
        }

        public void Editar(EventoTipoVm model, string usuarioLogado)
        {
            var eventoTipo = _eventoTipoRepository.ObterPorId(model.Id);

            eventoTipo.Alterar(model.Nome);
            
            _eventoTipoRepository.Update(eventoTipo);

            Save(usuarioLogado);
        }

        public IEnumerable<EventoTipoVm> SelecionarTodosAtivos()
        {
            var eventoTipoList  = _eventoTipoRepository.GetAllActive();

            return eventoTipoList.Select(eventoTipo => new EventoTipoVm(eventoTipo)).ToList();

        }

        public EventoTipoVm ObterPorId(int id)
        {
            var eventoTipo = _eventoTipoRepository.ObterPorId(id);

            return new EventoTipoVm(eventoTipo);
        }
    }
}
