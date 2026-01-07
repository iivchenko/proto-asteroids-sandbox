using Engine;
using Engine.EFS;
using Game.EFS.Entities;

namespace Game.EFS;

public sealed class GameBootstrapScene : IScene
{
    private readonly IWorld _world;
    private readonly IEntityBuilderFactory<AsteroidBuilder> _asteroidBuilderFactory;

    public GameBootstrapScene(
        IEntityBuilderFactory<AsteroidBuilder> asteroidsBuilderFactory, 
        IWorld world)
    {
        _world = world;
        _asteroidBuilderFactory = asteroidsBuilderFactory;

        _world.AddEntity(CreateRandomAsteroid());
        _world.AddEntity(CreateRandomAsteroid());
        _world.AddEntity(CreateRandomAsteroid());
    }

    public void Process(float time)
    {
        _world.Process(time);
    }

    private Asteroid CreateRandomAsteroid()
    {
        return _asteroidBuilderFactory
                .Create()
                .WithRandomType()
                .WithRandomPosition()
                .WithRandomDirection()
                .Build();
    }
}
