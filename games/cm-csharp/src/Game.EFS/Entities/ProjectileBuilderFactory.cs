using Engine;
using Engine.EFS;
using Engine.Services;

namespace Game.EFS.Entities;

public sealed class ProjectileBuilderFactory(
   IAssetService<Sprite> spriteLoader,
   IRandomService randomService,
   IViewService viewService)
    : IEntityBuilderFactory<ProjectileBuilder>
{
    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;
    private readonly IRandomService _randomService = randomService;
    private readonly IViewService _viewService = viewService;

    public ProjectileBuilder Create()
    {
        return new ProjectileBuilder(_spriteLoader, _randomService, _viewService);
    }
}