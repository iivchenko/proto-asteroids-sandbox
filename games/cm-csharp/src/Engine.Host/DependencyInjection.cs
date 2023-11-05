using Engine.Host;
using Engine.Graphics;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection WithHostServices(this IServiceCollection services)
    {
        services
            .AddSingleton<Game>()
            .AddSingleton<IGame>(x => x.GetRequiredService<Game>())
            .AddSingleton<IPainter>(x => x.GetRequiredService<Game>());

        return services;
    }
}

