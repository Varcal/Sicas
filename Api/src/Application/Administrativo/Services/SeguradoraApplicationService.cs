using System.Collections.Generic;
using System.IO;
using System.Linq;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Seguradoras;
using Application.Core.Services;
using CrossCutting.Utils.Helpers;
using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Entities;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class SeguradoraApplicationService: ApplicationService, ISeguradoraApplicationService
    {

        private readonly ISeguradoraRepository _seguradoraRepository;

        public SeguradoraApplicationService(IUnitOfWork uow, ISeguradoraRepository seguradoraRepository) : base(uow)
        {
            _seguradoraRepository = seguradoraRepository;
        }

        

        public void Registrar(SeguradoraVm model, string user)
        {

            try
            {
                var diretorio = Directories.CreateUploadDirectories(model.Cnpj);
                var seguradora = model.ToSeguradora(model, diretorio);
                seguradora.IsValid();

                _seguradoraRepository.Add(seguradora);
                Save(user);
            }
            catch (IOException ex)
            {
                throw ex;
            }
           
        }

        public void Editar(SeguradoraVm model, string user)
        {
            var seguradora = _seguradoraRepository.GetById(model.Id);

            var eventos = model.Eventos.Select(eventoViewModel => new Evento(eventoViewModel.ServicoSeguradoraId, eventoViewModel.EventoTipoId, eventoViewModel.FranquiaKm, eventoViewModel.Honorario)).ToList();

            seguradora.Alterar(model.Nome, model.Cnpj, model.Telefone, model.Contato, model.Email, model.ValorPorKm, model.Observacoes, eventos);

            _seguradoraRepository.Update(seguradora);

            Save(user);
        }

        public void Excluir(int id, string user)
        {
            var seguradora = _seguradoraRepository.GetById(id);
            _seguradoraRepository.Delete(seguradora);

            Save(user);
        }

        public IEnumerable<SeguradoraVm> SelecionarTodosAtivos()
        {
            var seguradoras = _seguradoraRepository.GetAllActive();

            return seguradoras.Select(seguradora => new SeguradoraVm(seguradora)).ToList();
        }

        public SeguradoraVm ObterPorId(int id)
        {
            var seguradora = _seguradoraRepository.GetById(id);

            var model = new SeguradoraVm(seguradora);

            return model;
        }

        
    }
}
