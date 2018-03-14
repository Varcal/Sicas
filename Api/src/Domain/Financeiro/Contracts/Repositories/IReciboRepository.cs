using System.Collections.Generic;
using Domain.Core.Contracts.Repositories.Base;
using Domain.Financeiro.Entities.RecibosSindicantes;

namespace Domain.Financeiro.Contracts.Repositories
{
    public interface IReciboRepository: IRepositoryBase<Recibo>
    {
        Recibo ObterRecibo(int sindicanteId, int processoId);
        Recibo ObterId(int id);
        IEnumerable<Recibo> SelecionarPorIds(List<int> recibosIds);
    }
}
