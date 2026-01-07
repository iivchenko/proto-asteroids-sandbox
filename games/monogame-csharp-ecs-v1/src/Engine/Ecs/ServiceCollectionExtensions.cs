using Engine.Ecs.Core;
using Engine.Ecs.Systems;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEcs(this IServiceCollection services)
    {
        return
            services
                .AddSingleton<ISystem, DrawSystem>()
                .AddTransient<IWorld, World>();
    }
}
