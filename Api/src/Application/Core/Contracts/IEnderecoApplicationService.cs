using Application.Core.ViewModels;

namespace Application.Core.Contracts
{
    public interface IEnderecoApplicationService
    {
        void Cadastrar(EnderecoVm endereco, string usuarioLogado);
    }
}
