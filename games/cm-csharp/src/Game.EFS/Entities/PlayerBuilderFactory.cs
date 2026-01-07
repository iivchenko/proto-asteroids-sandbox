using Engine;
using Engine.EFS;
using Engine.Services;

namespace Game.EFS.Entities;

public sealed class PlayerBuilderFactory(
   IAssetService<Sprite> spriteLoader,
   IViewService viewService)
    : IEntityBuilderFactory<PlayerBuilder>
{
    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;
    private readonly IViewService _viewService = viewService;

    public PlayerBuilder Create()
    {
        return new PlayerBuilder(_spriteLoader, _viewService);
    }
}
