namespace Engine.EFS;

public sealed class AddEntityCommand(Entity entity) : IWorldCommand
{
    public Entity Entity { get; } = entity;
}

