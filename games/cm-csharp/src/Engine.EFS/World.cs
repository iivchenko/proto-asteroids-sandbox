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
        var commands = new List<IWorldCommand>();

        foreach (var system in _systems)
        {
            commands.AddRange(system.Process(_entities, delta));
        }

        foreach (var command in commands)
        {
            switch (command)
            {
                case AddEntityCommand worldCommand:
                    _entities.Add(worldCommand.Entity);
                    break;

                case RemoveEntityCommand worldCommand:
                    _entities.Remove(worldCommand.Entity);
                    break;
            }
        }

        commands.Clear();
    }
}
