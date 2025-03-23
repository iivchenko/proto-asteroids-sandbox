using Engine;
using Engine.Entities;
using Engine.Services;

namespace Game.Entities;

public sealed class AsteroidsBuilderFactory : IEntityBuilderFactory<AsteroidBuilder>
{
    private readonly IGraphicsService _draw;
    private readonly IAssetService<Sprite> _spriteLoader;

    public AsteroidsBuilderFactory(
       IAssetService<Sprite> spriteLoader,
       IGraphicsService draw)
    {
        _spriteLoader = spriteLoader;
        _draw = draw;
    }

    public AsteroidBuilder Create()
    {
        return new AsteroidBuilder(_spriteLoader, _draw);
    }
}
