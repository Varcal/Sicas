namespace Domain.Core.Entities
{
    public abstract class EntityId: EntityBase
    {
        public int Id { get; protected set; }
    }
}
