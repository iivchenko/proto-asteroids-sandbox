using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Game.EFS.Entities;

namespace Game.EFS;

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