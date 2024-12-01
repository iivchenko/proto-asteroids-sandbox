using Microsoft.Extensions.DependencyInjection;

namespace Engine;

public sealed class GameBuilder
{
    private readonly ServiceCollection _container;

    private GameBuilder()
    {
        _container = new ServiceCollection();
    }

    public static GameBuilder CreateBuilder()
    {
        return new GameBuilder();
    }

    public GameBuilder WithServices(Action<IServiceCollection> configure)
    {
        configure(_container);

        return this;
    }

    public IGame Build()
    {
        var provider = _container.BuildServiceProvider();

        return provider.GetRequiredService<IGame>();
    }
}
