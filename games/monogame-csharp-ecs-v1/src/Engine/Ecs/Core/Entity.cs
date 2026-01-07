namespace Engine.Ecs.Core;

public sealed class Entity(Guid id, IEnumerable<IComponent> components) : IEntity
{
    public Guid Id { get; } = id;

    public IEnumerable<IComponent> Components { get; } = components;
}