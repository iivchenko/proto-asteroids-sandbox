using Microsoft.Extensions.DependencyInjection;

namespace Engine.Host;

public sealed class GameBuilder
{
    private readonly ServiceCollection _container;
    private readonly GameConfiguration _configuration;
    private Type _scene; // TODO: I think to set this up with default scene which will show message "No scene configured"

    private GameBuilder()
    {
        _container = new ServiceCollection();
        _configuration = new GameConfiguration();
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

    public GameBuilder WithConfiguration(Action<GameConfiguration> configure)
    {
        configure(_configuration);

        return this;
    }

    public GameBuilder WithBootstrapScene<TScene>()
        where TScene : class, IScene
    {
        _scene = typeof(TScene);

        return this;
    }

    public IGame Build()
    {
        _container.AddSingleton(_configuration);
        _container.AddSingleton(x => new SceneBootstraper(x, _scene));

        return _container
            .BuildServiceProvider()
            .GetRequiredService<IGame>();
    }
}
