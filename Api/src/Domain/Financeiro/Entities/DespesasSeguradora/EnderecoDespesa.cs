using Domain.Core.Entities;
using Domain.Core.Entities.Enderecos;

namespace Domain.Financeiro.Entities.DespesasSeguradora
{
    public class EnderecoDespesa: EntityId
    {
        public int EnderecoId { get; private set; }
        public string Numero { get; private set; }

        public Endereco Endereco { get; private set; }


        protected EnderecoDespesa()
        {
            
        }

        public EnderecoDespesa(Endereco endereco, string numero)
        {
            EnderecoId = endereco?.Id ?? 0;
            Endereco = endereco;
            Numero = numero;
        }

        public void Alterar(EnderecoDespesa endereco)
        {
            EnderecoId = endereco.EnderecoId;
            Numero = endereco.Numero;
        }
    }
}
