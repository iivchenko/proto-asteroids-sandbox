using Microsoft.Extensions.DependencyInjection;

namespace Engine.Game;

public sealed class GameBuilder
{
    private readonly ServiceCollection _container;
    private readonly Configuration _configuration;
    private Type _scene;

    private GameBuilder()
    {
        _container = new ServiceCollection();
        _configuration = new Configuration();
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

    public GameBuilder WithConfiguration(Action<Configuration> configure)
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
