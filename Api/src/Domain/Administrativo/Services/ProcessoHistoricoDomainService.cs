using Domain.Administrativo.Contracts.Repositories;
using Domain.Administrativo.Contracts.Services;
using Domain.Administrativo.Entities;

namespace Domain.Administrativo.Services
{
    public class ProcessoHistoricoDomainService : IProcessoHistoricoDomainService
    {

        private readonly IProcessoHstoricoRepository _processoHstoricoRepository;

        public ProcessoHistoricoDomainService(IProcessoHstoricoRepository processoHstoricoRepository)
        {
            _processoHstoricoRepository = processoHstoricoRepository;
        }

        public void FinalizarHistorico(Processo processo)
        {
            var historicoEmAberto = _processoHstoricoRepository.ObterHistoricoEmAberto(processo.Id);
            historicoEmAberto.FinalizarHistorico();
            _processoHstoricoRepository.Update(historicoEmAberto);
        }
    }
}
