namespace Domain.Core.Entities.DadosCadastros
{
    public class DadosPessoaFisica: DadosCadastrais
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Rg { get; private set; }

        protected DadosPessoaFisica()
        {
            
        }

        public DadosPessoaFisica(string nome, string cpf, string rg)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
        }

        internal void Alterar(string nome, string cpf, string rg)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
        }
    }
}
