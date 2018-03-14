using Application.Core.Contracts;
using Application.Core.ViewModels;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Entities.Enderecos;
using Infra.Data.Core.UnitOfWork;

namespace Application.Core.Services
{
    public class EnderecoApplicationService: ApplicationService, IEnderecoApplicationService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoApplicationService(IUnitOfWork uow, IEnderecoRepository enderecoRepository) 
            : base(uow)
        {
            _enderecoRepository = enderecoRepository;
        }

        public void Cadastrar(EnderecoVm model, string usuarioLogado)
        {

            if (_enderecoRepository.Existe(model.Cep, model.Logradouro))
            {
                return;
            }

            var endereco = new Endereco(model.Logradouro, model.Cep, model.Bairro, model.CidadeId);

            _enderecoRepository.Add(endereco);

            Save(usuarioLogado);
        }
    }
}
