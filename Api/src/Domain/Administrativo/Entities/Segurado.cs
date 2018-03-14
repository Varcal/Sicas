using Domain.Core.Entities;

namespace Domain.Administrativo.Entities
{
    public class Segurado: EntityId
    {
        public string Nome { get; private set; }
        public int? EnderecoSeguradoId { get; private set; }
        public EnderecoSegurado EnderecoSegurado { get; private set; }


        protected Segurado()
        {
            
        }

        public Segurado(string nome)
        {
            Nome = nome;
            //EnderecoSeguradoId = endereco?.Id ?? 0;
            //EnderecoSegurado = endereco;
        }

        public void Alterar(Segurado segurado)
        {
            Nome = segurado.Nome;
            //EnderecoSegurado.Alterar(segurado.EnderecoSegurado.Endereco, segurado.EnderecoSegurado.EnderecoTipoId, segurado.EnderecoSegurado.Numero, segurado.EnderecoSegurado.Complemento);
        }
    }
}