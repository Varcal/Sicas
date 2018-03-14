using System.Collections.Generic;
using Domain.Administrativo.Entities;

namespace Domain.Administrativo.Contracts.Services
{
    public interface IEnderecoDomainService
    {
        IList<EnderecoSindicante> RetornaEnderecoSindicate(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento);
        EnderecoSegurado RetornaEnderecoSegurado(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento);
        LocalFatos RetornaLocalFatos(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento);
    }
}
