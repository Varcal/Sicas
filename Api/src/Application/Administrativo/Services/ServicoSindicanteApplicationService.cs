using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.ServicosSindicantes;
using Application.Core.Services;
using Domain.Administrativo.Contracts.Repositories;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class ServicoSindicanteApplicationService: ApplicationService, IServicoSindicanteApplicationService
    {
        private readonly IServicoSindicanteRepository _servicoSindicanteRepository;

        public ServicoSindicanteApplicationService(IUnitOfWork uow, IServicoSindicanteRepository servicoSindicanteRepository) : base(uow)
        {
            _servicoSindicanteRepository = servicoSindicanteRepository;
        }

        public IEnumerable<ServicoSindicanteVm> SelecionarPorSindicante(int id)
        {
            var servicosSindicantes = _servicoSindicanteRepository.SelecionarPorSindicante(id);

            return servicosSindicantes.Select(servicoSindicante => new ServicoSindicanteVm(servicoSindicante)).ToList();
        }

        public IEnumerable<ServicoSindicanteVm> SelecionarTodosAtivos()
        {
            var servicosSindicantes = _servicoSindicanteRepository.GetAllActive();

            return servicosSindicantes.Select(servicoSindicante => new ServicoSindicanteVm(servicoSindicante)).ToList();
        }
    }
}
