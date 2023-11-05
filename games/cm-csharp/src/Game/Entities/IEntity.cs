namespace Game.Entities
{
    public interface IEntity<TId> : IEntity
    {
        TId Id { get; }
    }

    public interface IEntity
    {
        IEnumerable<string> Tags { get; }
    }
}
