using Engine;
using Engine.Services;
using Game.Assets;

namespace Game.EFS.Entities;

public sealed class PlayerBuilder(
    IAssetService<Sprite> spriteLoader,
    IViewService viewService)
{
    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;
    private readonly IViewService _viewService = viewService;

    public Player Build()
    {
        var sprite = _spriteLoader.Load(AssetStore.Sprites.PlayerShips.PlayerShip01_png.Path);
        var view = _viewService.GetView();
        var position = new Vec(
            view.Width / 2.0f - sprite.Width / 2.0f,
            view.Height / 2.0f - sprite.Height / 2.0f);

        return new Player(
            sprite,
            Vec.Zero,
            Vec.One,
            Angle.Zero,
            position);
    }
}
