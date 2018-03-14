using Domain.Administrativo.Entities;

namespace Domain.Administrativo.Contracts.Services
{
    public interface IProcessoHistoricoDomainService
    {
        void FinalizarHistorico(Processo processo);
    }
}
