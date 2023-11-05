namespace Engine.Entities;

public interface IEntityBuilderFactory<TBuilder>
{
    TBuilder Create();
}