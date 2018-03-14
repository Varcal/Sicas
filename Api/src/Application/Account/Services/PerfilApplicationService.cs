using System.Collections.Generic;
using System.Linq;
using Application.Account.Contracts;
using Application.Account.ViewModels.Perfis;
using Application.Core.Services;
using Domain.Account.Contracts.Repositories;
using Infra.Data.Core.UnitOfWork;

namespace Application.Account.Services
{
    public class PerfilApplicationService: ApplicationService, IPerfilApplicationService
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilApplicationService(IUnitOfWork uow, IPerfilRepository perfilRepository) : base(uow)
        {
            _perfilRepository = perfilRepository;
        }

        public IEnumerable<PerfilVm> SelecionarTodosAtivos()
        {
            var perfis = _perfilRepository.GetAllActive();

            return perfis.Select(perfil => new PerfilVm(perfil)).ToList();
        }
    }
}
