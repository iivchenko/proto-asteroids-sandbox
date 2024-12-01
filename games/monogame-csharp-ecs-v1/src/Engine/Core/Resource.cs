namespace Engine.Core;

public abstract class Resource
{
    protected Resource()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}