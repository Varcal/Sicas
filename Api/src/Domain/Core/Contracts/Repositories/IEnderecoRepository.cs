using Domain.Core.Contracts.Repositories.Base;
using Domain.Core.Entities.Enderecos;

namespace Domain.Core.Contracts.Repositories
{
    public interface IEnderecoRepository: IRepositoryBase<Endereco>
    {
        Endereco RetornaSeCadastrado(string cep, string endederco);
        bool Existe(string cep, string logradouro);
    }
}
