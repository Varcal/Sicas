namespace Domain.Core.Entities.Enderecos
{
    public class Cidade: EntityId
    {
        public string Nome { get; private set; }
        public int  EstadoId { get; private set; }
        public Estado Estado { get; private set; }
    }
}
