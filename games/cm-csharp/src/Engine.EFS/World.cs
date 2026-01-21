using Engine.EFS.Systems;

namespace Engine.EFS;

public sealed class World(IEnumerable<ISystem> systems) : IWorld
{
    private readonly List<IEntity> _entities = [];
    private readonly IEnumerable<ISystem> _systems = systems;

    public void AddEntity(IEntity entity)
    {
        _entities.Add(entity);
    }

    public void Process(float delta)
    {
        foreach (var system in _systems)
        {
            system.Process(_entities, delta);
        }

        _entities.RemoveAll(entity => !entity.IsAlive);
    }
}
