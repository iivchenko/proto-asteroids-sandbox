using Microsoft.Extensions.DependencyInjection;

namespace Engine.Host;

public sealed class SceneBootstraper(IServiceProvider provider, Type scene)
{
    private readonly IServiceProvider _provider = provider;
    private readonly Type _scene = scene;

    public IScene Create()
    {
        return (IScene)ActivatorUtilities.CreateInstance(_provider, _scene);
    }
}