using System.Collections.Generic;
using Domain.Account.Entities;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories.Base;

namespace Domain.Administrativo.Contracts.Repositories
{
    public  interface ISindicanteRepository: IRepositoryBase<Sindicante>
    {
        IEnumerable<Sindicante> SelecionarTodos();
        Sindicante ObterPorId(int id);
        SindicanteInterno ObterInternoPorId(int id);
        Usuario RetornarUsuario(int sindicanteId);
        IEnumerable<Sindicante> SelecionarTodosExternosAtivos();
        IEnumerable<Processo> ObterProcessos(int sindicanteId);
        IEnumerable<Sindicante> SelecionarPorProcesso(int processoId);
        bool VerificarSeSindicanteInterno(int id);
    }
}
