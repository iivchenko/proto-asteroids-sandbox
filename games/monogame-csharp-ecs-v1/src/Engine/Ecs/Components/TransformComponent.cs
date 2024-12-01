using Engine.Core;
using Engine.Ecs.Core;

namespace Engine.Ecs.Components;

public sealed class TransformComponent(Vector position) : IComponent
{
    public Vector Position { get; } = position;
}
