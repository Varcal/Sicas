using System.Collections.Generic;
using Domain.Administrativo.Contracts.Services;
using Domain.Administrativo.Entities;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Entities.Enderecos;
using SharedKernel.Enums;

namespace Domain.Administrativo.Services
{
    public class EnderecoDomainService : IEnderecoDomainService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoDomainService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public EnderecoSegurado RetornaEnderecoSegurado(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento)
        {
            var endereco = _enderecoRepository.RetornaSeCadastrado(cep, logradouro) ?? new Endereco(logradouro, cep, bairro, cidadeId);

            return new EnderecoSegurado(endereco, (int)EnderecoTipoEnum.Residencial, numero, complemento);
        }

        public IList<EnderecoSindicante> RetornaEnderecoSindicate(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento)
        {
            var enderecoList = new List<EnderecoSindicante>();

            var endereco = _enderecoRepository.RetornaSeCadastrado(cep, logradouro) ?? new Endereco(logradouro, cep, bairro, cidadeId);

            enderecoList.Add(new EnderecoSindicante(endereco, (int)EnderecoTipoEnum.Residencial, numero, complemento));

            return enderecoList;            
        }

        public LocalFatos RetornaLocalFatos(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento)
        {
            var endereco = _enderecoRepository.RetornaSeCadastrado(cep, logradouro) ?? new Endereco(logradouro, cep, bairro, cidadeId);

            return new LocalFatos(endereco, (int)EnderecoTipoEnum.Residencial, numero, complemento);
        }
    }
}
