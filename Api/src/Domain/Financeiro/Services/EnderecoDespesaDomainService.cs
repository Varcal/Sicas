using Domain.Core.Contracts.Repositories;
using Domain.Core.Entities.Enderecos;
using Domain.Financeiro.Contracts.Services;
using Domain.Financeiro.Entities.DespesasSeguradora;

namespace Domain.Financeiro.Services
{
    public class EnderecoDespesaDomainService: IEnderecoDespesaDomainService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoDespesaDomainService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }


        public EnderecoDespesa RetornEnderecoDespesa(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento)
        {
          
            var endereco = _enderecoRepository.RetornaSeCadastrado(cep, logradouro) ?? new Endereco(logradouro, cep, bairro, cidadeId);

            return new EnderecoDespesa(endereco, numero);
        }
    }
}
