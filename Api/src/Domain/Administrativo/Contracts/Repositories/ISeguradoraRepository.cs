using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface ISeguradoraRepository: IRepositoryBase<Seguradora>
    {
        Seguradora GetById(int id);

        string RetornaUrlAdministrativo(int seguradoraId);
    }
}
