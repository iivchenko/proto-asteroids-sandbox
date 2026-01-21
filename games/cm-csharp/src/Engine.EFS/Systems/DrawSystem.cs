using Engine.EFS.Faces;
using Engine.Services;
using Engine.Utilities;

namespace Engine.EFS.Systems;

public sealed class DrawSystem(IGraphicsService graphicsService) : ISystem
{
    public readonly IGraphicsService _graphicsService = graphicsService;

    public IEnumerable<IWorldCommand> Process(IEnumerable<IEntity> faces, float delta)
    {
        faces
            .Where(face => face is not null)
            .Where(face => face is IDrawableFace)
            .Cast<IDrawableFace>()
            .Iter(face =>
            {
                _graphicsService
                   .Draw(
                       face.Sprite,
                       face.Position,
                       face.Origin,
                       face.Scale,
                       face.Rotation,
                       Colors.White);
            });

        return [];
    }
}
