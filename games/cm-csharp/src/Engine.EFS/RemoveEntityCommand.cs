namespace Engine.EFS;

public sealed class RemoveEntityCommand(Entity entity) : IWorldCommand
{
    public Entity Entity { get; } = entity;
}