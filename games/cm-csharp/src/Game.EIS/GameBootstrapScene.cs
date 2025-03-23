using Engine;
using Engine.EIS;
using Game.EIS.Entities;

namespace Game.EIS;

public sealed class GameBootstrapScene : IScene
{
    private Asteroid _asteroid;

    public GameBootstrapScene(IEntityBuilderFactory<AsteroidBuilder> asteroidsBuilderFactory)
    {
        _asteroid = 
            asteroidsBuilderFactory
                .Create()
                .WithType(AsteroidType.Medium)
                .Build(new Vec(0, 800), new Angle(-100, AngleType.Degrees));
    }

    public void Update(float time)
    {
        ((IUpdatable)_asteroid).Update(time);
        ((IDrawable)_asteroid).Draw(time);
    }
}