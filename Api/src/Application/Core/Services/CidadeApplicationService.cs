using System.Collections.Generic;
using System.Linq;
using Application.Core.Contracts;
using Application.Core.ViewModels;
using Domain.Core.Contracts.Repositories;
using Infra.Data.Core.UnitOfWork;

namespace Application.Core.Services
{
    public class CidadeApplicationService: ApplicationService, ICidadeApplicationService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeApplicationService(IUnitOfWork uow, ICidadeRepository cidadeRepository) 
            :base(uow)
        {
            _cidadeRepository = cidadeRepository;
        }

        public IEnumerable<CidadeVm> SelecionarPorEstado(int ufId)
        {
            var cidades = _cidadeRepository.SelecionarPorEstado(ufId);
            
            return cidades.Select(cidade => new CidadeVm(cidade)).ToList();
        }
    }
}
