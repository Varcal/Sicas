using Domain.Core.Contracts.Repositories.Base;
using Domain.Administrativo.Entities;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IEventoRepository: IRepositoryBase<Evento>
    {
        int ObterFranquiaKm(int seguradoraId, int servicoSeguradoraId, int tipoEvento);
    }
}
