using Engine;
using Engine.EFS;
using Engine.Services;

namespace Game.EFS.Entities;

public sealed class AsteroidsBuilderFactory(
   IAssetService<Sprite> spriteLoader,
   IRandomService randomService,
   IViewService viewService) 
    : IEntityBuilderFactory<AsteroidBuilder>
{
    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;
    private readonly IRandomService _randomService = randomService;
    private readonly IViewService _viewService = viewService;

    public AsteroidBuilder Create()
    {
        return new AsteroidBuilder(_spriteLoader, _randomService, _viewService);
    }
}
