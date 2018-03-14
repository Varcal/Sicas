using System.Collections.Generic;
using System.Linq;
using Application.Core.Contracts;
using Application.Core.Services;
using Application.Core.ViewModels;
using Domain.Core.Contracts.Repositories;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class EstadoApplicationService: ApplicationService, IEstadoApplicationService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoApplicationService(IUnitOfWork uow, IEstadoRepository estadoRepository) 
            :base(uow)
        {
            _estadoRepository = estadoRepository;
        }

        public IEnumerable<EstadoVm> SelecionarTodos()
        {
            var estados = _estadoRepository.SelecionarTodos();

            return estados.Select(estado => new EstadoVm(estado)).ToList();
        }
    }
}
