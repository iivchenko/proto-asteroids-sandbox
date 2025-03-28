﻿using Microsoft.Extensions.DependencyInjection;

namespace Engine.Host;

public sealed class SceneBootstraper
{
    private readonly IServiceProvider _provider;
    private readonly Type _scene;

    public SceneBootstraper(IServiceProvider provider, Type scene)
    {
        _provider = provider;
        _scene = scene;
    }

    public IScene Create()
    {
        return (IScene)ActivatorUtilities.CreateInstance(_provider, _scene);
    }
}