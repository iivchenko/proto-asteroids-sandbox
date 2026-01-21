using Engine.EFS.Faces;

namespace Engine.EFS.Systems;

public sealed class CollideSystem : ISystem
{
    public void Process(IEnumerable<IEntity> faces, float delta)
    {
        var collidables = faces
            .Where(face => face is not null)
            .Where(face => face is ICollidableFace)
            .Cast<ICollidableFace>()
            .Where(face => face.IsCollidable)
            .ToList();

        for (int i = 0; i < collidables.Count; i++)
        {
            var face1 = collidables[i];

            for (var j = i + 1; j < collidables.Count; j++)
            {
                var face2 = collidables[j];

                if (IsColliding(face1, face2))
                {
                    // TODO: Implement Pixel Perfect Collision Detection
                    face1.OnCollide(face2);
                    face2.OnCollide(face1);
                }
            }
        }
    }

    private static bool IsColliding(ICollidableFace face1, ICollidableFace face2)
    {
        var left1 = face1.Position.X - face1.Origin.X * face1.Scale.X;
        var right1 = face1.Position.X - face1.Origin.X * face1.Scale.X + face1.Width * face1.Scale.X;
        var top1 = face1.Position.Y - face1.Origin.Y * face1.Scale.X;
        var bottom1 = face1.Position.Y - face1.Origin.Y * face1.Scale.X + face1.Height * face1.Scale.X;

        var left2 = face2.Position.X - face2.Origin.X * face2.Scale.X;
        var right2 = face2.Position.X - face2.Origin.X * face2.Scale.X + face2.Width * face2.Scale.X;
        var top2 = face2.Position.Y - face2.Origin.Y * face2.Scale.X;
        var bottom2 = face2.Position.Y - face2.Origin.Y * face2.Scale.X + face2.Height * face2.Scale.X;

        return
            left1 < right2 && left2 < right1 &&
            top1 < bottom2 && top2 < bottom1;
    }
}