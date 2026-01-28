using Engine;
using Engine.EFS;
using Game.EFS.Entities;

namespace Game.EFS;

public sealed class GameBootstrapScene : IScene
{
    private readonly IWorld _world;
    private readonly IEntityBuilderFactory<PlayerBuilder> _playerBuilderFactory;

    public GameBootstrapScene(
        IEntityBuilderFactory<PlayerBuilder> playerBuilderFactory,
        IWorld world)
    {
        _world = world;
        _playerBuilderFactory = playerBuilderFactory;

        _world.AddEntity(CreatePlayer());
    }

    public void Process(float time)
    {
        _world.Process(time);
    }

    private Player CreatePlayer()
    {
        return _playerBuilderFactory.Create().Build();
    }
}
