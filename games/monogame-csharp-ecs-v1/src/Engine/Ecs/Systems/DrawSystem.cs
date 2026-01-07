using Engine.Ecs.Components;
using Engine.Ecs.Core;
using Engine.Services.Draw;

namespace Engine.Ecs.Systems;

public sealed class DrawSystem(IDrawService drawService) : ISystem
{
    private readonly IDrawService _drawService = drawService;

    public void Process(IEnumerable<IEntity> entities, float delta)
    {
        foreach (var entity in entities.Where(entity => entity.WithComponents<SpriteComponent, TransformComponent>()))
        {
            var (spriteComponent, transformComponent) = entity.FetchComponents<SpriteComponent, TransformComponent>();

            _drawService.Draw(spriteComponent.Sprite, transformComponent.Position, spriteComponent.Tint);
        }
    }
}
