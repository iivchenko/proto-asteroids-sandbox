using Engine;

namespace Desktop;

public sealed class EntryPoint(IScene scene) : IEntryPoint
{
    private readonly IScene _scene = scene;

    public void Process(float delta)
    {
        _scene.Process(delta);
    }
}
