using Engine.Host;
using Engine.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Backends.Sfml;

public static class GameBuilderExtensions
{
    public static GameBuilder WithSfmlBackend(this GameBuilder builder)
    {
        builder.WithServices(services =>
        {
            services.AddSingleton<SfmlGame>();
            services.AddSingleton<IGame>(x => x.GetRequiredService<SfmlGame>());
            services.AddSingleton<IAssetService<Sprite>>(x => x.GetRequiredService<SfmlGame>());
            services.AddSingleton<IGraphicsService>(x => x.GetRequiredService<SfmlGame>());
        });

        return builder;
    }
}

