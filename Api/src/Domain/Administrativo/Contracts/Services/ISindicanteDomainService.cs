using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain.Administrativo.Entities;

namespace Domain.Administrativo.Contracts.Services
{
    public interface ISindicanteDomainService
    {
        Sindicante Registrar(int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular,
            string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> endereco,
            ICollection<Honorario> honorarios, [Optional] decimal valorPorKm);

        Sindicante Alterar(int id, int sindicanteTipoId, string nome, string cpf, string rg, string telefone, string celular,
            string email, DadosBancarios dadosBancarios, ICollection<EnderecoSindicante> endereco,
            ICollection<Honorario> honorarios, [Optional] decimal valorPorKm);

        Sindicante Excluir(int id, int sindicanteTipo);
    }
}
