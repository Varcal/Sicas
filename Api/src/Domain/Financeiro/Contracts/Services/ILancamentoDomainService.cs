using System.Collections.Generic;
using Domain.Administrativo.Entities;

namespace Domain.Financeiro.Contracts.Services
{
    public interface ILancamentoDomainService
    {
        void EfetuarLancamentosDeDespesaAutomatico(Processo processo);
        void GerarLancamentoHonorario(Processo processo, IList<Sindicante> sindicantes);
    }
}
