namespace Engine.EFS;

public interface IEntityBuilderFactory<TBuilder>
{
    TBuilder Create();
}