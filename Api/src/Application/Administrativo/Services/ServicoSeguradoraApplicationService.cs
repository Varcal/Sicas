using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.ServicosSeguradoras;
using Application.Core.Services;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class ServicoSeguradoraApplicationService: ApplicationService, IServicoSeguradoraApplicationService
    {
        private readonly IServicoSeguradoraRepository _servicoSeguradoraRepository;
        private readonly IEventoTipoRepository _eventoTipoRepository;

        public ServicoSeguradoraApplicationService(IUnitOfWork uow, IServicoSeguradoraRepository servicoSeguradoraRepository, IEventoTipoRepository eventoTipoRepository) : base(uow)
        {
            _servicoSeguradoraRepository = servicoSeguradoraRepository;
            _eventoTipoRepository = eventoTipoRepository;
        }

        public void Registrar(ServicoSeguradoraVm model, string user)
        {
            var eventoTipoList = RetornarEventoTipos(model);
            var servicoSeguradora = model.ToServicoSeguradora(model, eventoTipoList);

            _servicoSeguradoraRepository.Add(servicoSeguradora);

            Save(user);
        }

        public void Alterar(ServicoSeguradoraVm model, string user)
        {
            var eventoTipoList = RetornarEventoTipos(model);
            var seguradoraServico = _servicoSeguradoraRepository.ObterPorId(model.Id);

            seguradoraServico.Alterar(model.Nome, eventoTipoList);
            _servicoSeguradoraRepository.Update(seguradoraServico);

            Save(user);
        }

        public IEnumerable<ServicoSeguradoraListVm> SelecionarTodosAtivos()
        {
            var servicoSeguradoraList = _servicoSeguradoraRepository.GetAllActive();

            return servicoSeguradoraList.Select(servicoSeguradora => new ServicoSeguradoraListVm(servicoSeguradora)).ToList();
        }

        
        public IEnumerable<ServicoSeguradoraVm> SelecionarTodosComEventos()
        {
            var servicoSeguradoraList = _servicoSeguradoraRepository.SelecionarTodosComEventos();

            return servicoSeguradoraList.Select(servicoSeguradora => new ServicoSeguradoraVm(servicoSeguradora)).ToList();
        }

        public ServicoSeguradoraVm ObterPorId(int id)
        {
            var servicoSeguradora = _servicoSeguradoraRepository.ObterPorId(id);

            return new ServicoSeguradoraVm(servicoSeguradora);
        }



        private List<EventoTipo> RetornarEventoTipos(ServicoSeguradoraVm model)
        {
            var eventoTipoIds = model.EventosTipos.Select(x => x.Id).ToList();
            var eventoTipoList = _eventoTipoRepository.SelecionarPorIds(eventoTipoIds).ToList();
            return eventoTipoList;
        }

        public void Excluir(int id, string usuarioLogado)
        {
            var seguradoraServico = _servicoSeguradoraRepository.ObterPorId(id);
            _servicoSeguradoraRepository.Delete(seguradoraServico);
            Save(usuarioLogado);
        }
    }
}
