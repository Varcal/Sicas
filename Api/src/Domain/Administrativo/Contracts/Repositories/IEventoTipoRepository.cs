using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IEventoTipoRepository: IRepositoryBase<EventoTipo>
    {
        IEnumerable<EventoTipo> SelecionarPorIds(IList<int> ids);
        EventoTipo ObterPorId(int id);
    }
}
