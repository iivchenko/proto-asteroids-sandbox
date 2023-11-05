namespace Engine.Assets;

public abstract class Asset
{
    protected Asset()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}
