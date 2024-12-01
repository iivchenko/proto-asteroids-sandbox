using Engine.Core;
using Engine.Ecs.Core;
using Engine.Services.Draw;

namespace Engine.Ecs.Components;

public sealed class SpriteComponent(Sprite sprite, Color tint) : IComponent
{
    public Sprite Sprite { get; } = sprite;

    public Color Tint { get; } = tint;
}