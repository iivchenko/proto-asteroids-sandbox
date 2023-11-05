using Engine;
using Game.Entities;

namespace Game;

public sealed class TempScene : IScene
{
    private IUpdatable _asteroid;

    public TempScene(IEntityFactory entityFactory)
    {
        _asteroid = entityFactory.CreateAsteroid(AsteroidType.Medium, new System.Numerics.Vector2(0, 800), -100);
    }

    public void Update(float time)
    {
        _asteroid.Update(time);
    }
}