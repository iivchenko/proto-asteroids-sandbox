using Engine;
using Engine.Entities;
using Game.Entities;
using System.Numerics;

namespace Game;

public sealed class GameBootstrapScene : IScene
{
    private Asteroid _asteroid;

    public GameBootstrapScene(IEntityBuilderFactory<AsteroidBuilder> asteroidsBuilderFactory)
    {
        _asteroid = 
            asteroidsBuilderFactory
                .Create()
                .WithType(AsteroidType.Medium)
                .Build(new Vector2(0, 800), -100);
    }

    public void Update(float time)
    {
        ((IUpdatable)_asteroid).Update(time);
        ((IDrawable)_asteroid).Draw(time);
    }
}