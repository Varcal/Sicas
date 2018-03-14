using System;
using System.Collections.Generic;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace Domain.Financeiro.Contracts.Repositories
{
    public interface IReciboSindicanteRepository : IRepositoryBase<ReciboSindicante>
    {
        ReciboSindicante ObterPorId(int id);
        IEnumerable<ReciboSindicante> SelecionarPorSindicanteData(DateTime dataInicio, DateTime dataFim);
        IEnumerable<ReciboSindicante> SelecionarTodosNaoEmitidosPorData(DateTime dataInicio, DateTime dataFim);
    }
}
