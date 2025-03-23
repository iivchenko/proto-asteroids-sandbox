﻿using Engine.EIS;
using Game.EIS.Entities;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection WithGameServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IEntityBuilderFactory<AsteroidBuilder>, AsteroidsBuilderFactory>();

        return services;
    }
}

