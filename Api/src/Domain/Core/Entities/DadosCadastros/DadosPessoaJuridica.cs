namespace Domain.Core.Entities.DadosCadastros
{
    public class DadosPessoaJuridica: DadosCadastrais
    {
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }
        public string InscEst { get; private set; }

        protected DadosPessoaJuridica()
        {
            
        }

        public DadosPessoaJuridica(string razaoSocial, string nomeFantasia, string cnpj, string inscEst)
        {
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            InscEst = inscEst;
        }

        public void Alterar(string nome, string cnpj)
        {
            RazaoSocial = nome;
            Cnpj = cnpj;
        }
    }
}
