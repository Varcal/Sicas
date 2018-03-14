using System.Collections.Generic;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public interface IServicoSeguradoraRepository: IRepositoryBase<ServicoSeguradora>
    {
        IEnumerable<ServicoSeguradora> SelecionarTodosComEventos();
        ServicoSeguradora ObterPorId(int id);
    }
}
