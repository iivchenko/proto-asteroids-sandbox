using Game;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection WithGameServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IEntityFactory, EntityFactory>();

        return services;
    }
}

