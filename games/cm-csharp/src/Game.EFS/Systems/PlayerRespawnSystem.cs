using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Engine.EFS.Systems;
using Engine.Services;
using Engine.Utilities;
using Game.EFS.Entities;

namespace Game.EFS.Systems;

public sealed class PlayerRespawnSystem(IViewService viewService) : ISystem
{
    private readonly IViewService _viewService = viewService;

    public IEnumerable<IWorldCommand> Process(IEnumerable<IEntity> faces, float delta)
    {
        faces
            .Where(face => face is Player)
            .Cast<Player>()
            .Where(player => player.State == PlayerState.Killed)
            .Iter(player =>
            {
                var view = _viewService.GetView();
                var visible = player as IDrawableFace;
                var collidable = player as ICollidableFace;
                var movable = player as IMovableFace;

                collidable.Position = new Vec(view.Width / 2.0f, view.Height / 2.0f);
                movable.LinearVelocity = Vec.Zero;

                player.State = PlayerState.Alive;
                visible.IsVisible = true;
                collidable.IsCollidable = true;
            });

        return [];
    }
}
