using System;
using System.Collections.Generic;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace Domain.Financeiro.Contracts.Repositories
{
    public interface ILancamentoRepository : IRepositoryBase<Lancamento>
    {
        Lancamento ObterPorId(int id);
        IEnumerable<Recibo> SelecionarPorSindicanteData(int sindicanteId, DateTime dataInicio, DateTime dataFim);
    }
}
