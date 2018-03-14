using Domain.Financeiro.Entities.DespesasSeguradora;

namespace Domain.Financeiro.Contracts.Services
{
    public interface IEnderecoDespesaDomainService
    {
        EnderecoDespesa RetornEnderecoDespesa(string logradouro, string numero, string cep, string bairro, int cidadeId, string complemento);
    }
}
