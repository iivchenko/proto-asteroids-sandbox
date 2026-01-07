using Engine.Services;

namespace Engine.Backends.Sfml;

public sealed partial class SfmlGame : IViewService
{
    public View GetView()
    {
        var viewport = _window.GetView().Viewport;

        return new View(viewport.Width, viewport.Height);
    }
}