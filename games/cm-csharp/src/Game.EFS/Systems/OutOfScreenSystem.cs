using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Engine.EFS.Systems;
using Engine.Services;
using Engine.Utilities;

namespace Game.EFS.Systems;

public sealed class OutOfScreenSystem(IViewService viewService) : ISystem
{
    private readonly IViewService _viewService = viewService;

    public void Process(IEnumerable<IEntity> faces, float delta)
    {
        var view = _viewService.GetView();

        faces
            .Where(face => face is not null)
            .Where(face => face is ICollidableFace)
            .Cast<ICollidableFace>()
            .Where(face => IsOutOfScreen(face, view))
            .Iter(face => MoveBack(face, view));
    }

    private static bool IsOutOfScreen(ICollidableFace face, View view)
    {
        return
            face.Position.X + face.Width / 2.0 < 0 ||
            face.Position.X - face.Width / 2.0 > view.Width ||
            face.Position.Y + face.Height / 2.0 < 0 ||
            face.Position.Y - face.Height / 2.0 > view.Height;
    }

    private static void MoveBack(ICollidableFace face, View view)
    {
        var x = face.Position.X;
        var y = face.Position.Y;

        if (face.Position.X + face.Width / 2.0f < 0)
        {
            x = view.Width + face.Width / 2.0f;
        }
        else if (face.Position.X - face.Width / 2.0f > view.Width)
        {
            x = 0 - face.Width / 2.0f;
        }

        if (face.Position.Y + face.Height / 2.0f < 0)
        {
            y = view.Height + face.Height / 2.0f;
        }
        else if (face.Position.Y - face.Height / 2.0f > view.Height)
        {
            y = 0 - face.Height / 2.0f;
        }

        face.Position = new Vec(x, y);
    }
}
