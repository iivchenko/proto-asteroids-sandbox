using Engine.Assets;
using Engine.Entities;
using Engine.Graphics;

namespace Game.Entities;

public sealed class AsteroidsBuilderFactory : IEntityBuilderFactory<AsteroidBuilder>
{
    private readonly IPainter _draw;
    private readonly IAssetProvider _assetProvider;

    public AsteroidsBuilderFactory(
       IAssetProvider assetProvider,
       IPainter draw)
    {
        _assetProvider = assetProvider;
        _draw = draw;
    }

    public AsteroidBuilder Create()
    {
        return new AsteroidBuilder(_assetProvider, _draw);
    }
}
