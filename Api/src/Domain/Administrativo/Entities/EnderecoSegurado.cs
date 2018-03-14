using System.Runtime.InteropServices;
using Domain.Core.Entities;
using Domain.Core.Entities.Enderecos;

namespace Domain.Administrativo.Entities
{
    public class EnderecoSegurado: EntityId
    {
        public int EnderecoTipoId { get; private set; }
        public int EnderecoId { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }

        public EnderecoTipo EnderecoTipo { get; private set; }
        public Endereco Endereco { get; private set; }

        public EnderecoSegurado()
        {

        }

        public EnderecoSegurado(Endereco endereco, int enderecoTipoId, string numero, [Optional]string complemento)
        {
            EnderecoTipoId = enderecoTipoId;
            EnderecoId = endereco != null ? endereco.Id : 0;
            Endereco = endereco;
            Numero = numero;
            Complemento = complemento;
        }


        public void Alterar(Endereco endereco, int enderecoTipoId, string numero, [Optional] string complemento)
        {
            EnderecoTipoId = enderecoTipoId;
            EnderecoId = endereco != null ? endereco.Id : 0;
            Endereco = endereco;
            Numero = numero;
            Complemento = complemento;
        }
    }
}