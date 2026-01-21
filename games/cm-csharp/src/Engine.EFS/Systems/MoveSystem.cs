using Engine.EFS.Faces;
using Engine.Utilities;

namespace Engine.EFS.Systems;

public sealed class MoveSystem : ISystem
{
    public IEnumerable<IWorldCommand> Process(IEnumerable<IEntity> faces, float delta)
    {
        faces
           .Where(face => face is not null)
           .Where(face => face is IMovableFace)
           .Cast<IMovableFace>()
           .Iter(face =>
           {
               face.Position += face.LinearVelocity * delta;
               face.Rotation += face.RotationVelocity * delta;
           });

        return [];
    }
}