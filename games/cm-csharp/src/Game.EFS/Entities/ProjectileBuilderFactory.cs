using Engine;
using Engine.EFS;
using Engine.Services;

namespace Game.EFS.Entities;

public sealed class ProjectileBuilderFactory(IAssetService<Sprite> spriteLoader)
    : IEntityBuilderFactory<ProjectileBuilder>
{
    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;

    public ProjectileBuilder Create()
    {
        return new ProjectileBuilder(_spriteLoader);
    }
}