namespace SharedKernel.Entities.Base
{
    public class Cidade: EntityId
    {
        #region Propriedades

        public string Nome { get; private set; }
        public int UfId { get; private set; }

        #region Navegação
        public Uf Uf { get; set; }
        #endregion

        #endregion
    }
}