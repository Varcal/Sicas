using System.Collections.Generic;
using System.Linq;
using Application.Administrativo.Contracts;
using Application.Administrativo.ViewModels.Sindicantes;
using Application.Core.Services;
using Domain.Administrativo.Contracts.Repositories;
using Infra.Data.Core.UnitOfWork;

namespace Application.Administrativo.Services
{
    public class BancoApplicationService : ApplicationService, IBancoApplicantionService
    {
        private readonly IBancoRepository _bancoRepository;

        public BancoApplicationService(IUnitOfWork uow, IBancoRepository bancoRepository) : base(uow)
        {
            _bancoRepository = bancoRepository;
        }

        public IEnumerable<BancoVm> SelecionarTodosAtivos()
        {
            
            var bancos = _bancoRepository.GetAllActive();

            return bancos.Select(banco => new BancoVm(banco)).ToList();

        }
    }
}
