using System.Runtime.InteropServices;
using Domain.Administrativo.Entities;
using Domain.Financeiro.Entities.DespesasSeguradora;

namespace Application.Core.ViewModels
{
    public class EnderecoVm
    {
        public int EnderecoTipoId { get; set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public int CidadeId { get; private set; }
        public string Complemento { get; private set; }
        public CidadeVm Cidade { get; set; }
        public EstadoVm Estado { get; set; }


        public EnderecoVm()
        {
            
        }

        public EnderecoVm(string logradouro, string numero, string cep, string bairro, int cidadeId, [Optional]string complemento)
        {
            EnderecoTipoId = 1;
            Logradouro = logradouro;
            Numero = numero;
            Cep = cep;
            Bairro = bairro;
            CidadeId = cidadeId;
            Complemento = complemento;
        }


        public EnderecoVm(EnderecoSindicante entity)
        {
            EnderecoTipoId = entity.EnderecoTipoId;
            Logradouro = entity.Endereco.Logradouro;
            Numero = entity.Numero;
            Cep = entity.Endereco.Cep;
            Bairro = entity.Endereco.Bairro;
            CidadeId = entity.Endereco.CidadeId;
            Complemento = entity.Complemento;
        }

        public EnderecoVm(EnderecoDespesa entity)
        {
            Logradouro = entity.Endereco.Logradouro;
            Numero = entity.Numero;
            Cep = entity.Endereco.Cep;
            Bairro = entity.Endereco.Bairro;
            CidadeId = entity.Endereco.CidadeId;
            Cidade = new CidadeVm(entity.Endereco.Cidade);
            Estado = new EstadoVm(entity.Endereco.Cidade.Estado);
        }

        public EnderecoVm(EnderecoSegurado entity)
        {
            Logradouro = entity.Endereco.Logradouro;
            Numero = entity.Numero;
            Cep = entity.Endereco.Cep;
            Bairro = entity.Endereco.Bairro;
            CidadeId = entity.Endereco.CidadeId;
            Cidade = new CidadeVm(entity.Endereco.Cidade);
            Estado = new EstadoVm(entity.Endereco.Cidade.Estado);
        }

        public EnderecoVm(LocalFatos entity)
        {
            Logradouro = entity.Endereco.Logradouro;
            Numero = entity.Numero;
            Cep = entity.Endereco.Cep;
            Bairro = entity.Endereco.Bairro;
            CidadeId = entity.Endereco.CidadeId;
            Cidade = new CidadeVm(entity.Endereco.Cidade);
            Estado = new EstadoVm(entity.Endereco.Cidade.Estado);
        }
    }
}
