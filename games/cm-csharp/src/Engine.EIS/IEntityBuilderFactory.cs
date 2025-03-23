namespace Engine.EIS;

public interface IEntityBuilderFactory<TBuilder>
{
    TBuilder Create();
}