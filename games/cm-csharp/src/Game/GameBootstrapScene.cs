using Engine;
using Engine.Entities;
using Game.Entities;

namespace Game;

public sealed class GameBootstrapScene : IScene
{
    private Asteroid _asteroid;

    public GameBootstrapScene(IEntityFactory entityFactory)
    {
        _asteroid = entityFactory.CreateAsteroid(AsteroidType.Medium, new System.Numerics.Vector2(0, 800), -100);
    }

    public void Update(float time)
    {
        ((IUpdatable)_asteroid).Update(time);
        ((IDrawable)_asteroid).Draw(time);
    }
}