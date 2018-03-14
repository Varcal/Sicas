using SharedKernel.Helpers;

namespace Domain.Core.Entities.Enderecos
{
    public class Endereco: EntityId
    {
        #region Propriedades

        public string Logradouro { get; private set; }       
        public string LogradouroFonetizado { get; private set; }      
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public int CidadeId { get; private set; }
        

        #region Navegação
        public Cidade Cidade { get; private set; }
        #endregion

        #endregion

        #region Construtores

        protected Endereco()
        {
            
        }

        public Endereco(string logradouro, string cep, string bairro, int cidadeId)
        {
            Logradouro = logradouro;
            LogradouroFonetizado = Soundex.FonetizarLogradouro(logradouro);
            Cep = cep;         
            Bairro = bairro;
            CidadeId = cidadeId;
        } 
        #endregion

    }
}
