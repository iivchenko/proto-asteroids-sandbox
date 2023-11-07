using Engine.Assets;
using Engine.Entities;
using Engine.Graphics;

namespace Game.Entities;

public sealed class AsteroidsBuilderFactory : IEntityBuilderFactory<AsteroidBuilder>
{
    private readonly IPainter _draw;
    private readonly IAssetLoader<Sprite> _spriteLoader;

    public AsteroidsBuilderFactory(
       IAssetLoader<Sprite> spriteLoader,
       IPainter draw)
    {
        _spriteLoader = spriteLoader;
        _draw = draw;
    }

    public AsteroidBuilder Create()
    {
        return new AsteroidBuilder(_spriteLoader, _draw);
    }
}
