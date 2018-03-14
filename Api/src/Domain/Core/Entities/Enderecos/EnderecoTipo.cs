namespace Domain.Core.Entities.Enderecos
{
    public class EnderecoTipo: EntityId
    {
        public string Nome { get; private set; }

        protected EnderecoTipo()
        {
            
        }

        public EnderecoTipo(string nome)
        {
            Nome = nome;
        }
    }
}