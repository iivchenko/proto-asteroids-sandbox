namespace Engine.EFS;

public sealed class EmptyEntityCommand : IWorldCommand
{
    public static readonly EmptyEntityCommand Empty = new();

    private EmptyEntityCommand() 
    {
    }
}