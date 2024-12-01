namespace Engine.Ecs.Core;

public interface IEntity
{
    public Guid Id { get; }

    public IEnumerable<IComponent> Components { get; }
}
