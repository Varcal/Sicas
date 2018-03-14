namespace Domain.Core.Entities.Enderecos
{
    public class Estado: EntityId
    {
        public string Nome { get; private set; }
        public string Uf { get; private set; }
        public int PaisId { get; private set; }

        public Pais Pais { get; set; }
    }
}