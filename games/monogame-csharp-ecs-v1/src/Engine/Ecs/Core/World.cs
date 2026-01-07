namespace Engine.Ecs.Core;

public sealed class World(IEnumerable<ISystem> systems) : IWorld
{
    private readonly List<ISystem> _systems = systems.ToList();
    private readonly List<IEntity> _entities = [];

    public void Process(float delta)
    {
        foreach (var system in _systems)
        {
            system.Process(_entities, delta);
        }
    }

    public IEntity CreateEntity(IEnumerable<IComponent> components) 
    {
        var entity = new Entity(Guid.NewGuid(), components);

        _entities.Add(entity);

        return entity;
    }
}
