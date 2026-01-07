using Engine.Services;

namespace Engine.Backends.Raylib;

public sealed class RayLibGame_ViewService : IViewService
{
    public View GetView()
    {
        return new View(Raylib_cs.Raylib.GetRenderWidth(), Raylib_cs.Raylib.GetRenderHeight());
    }
}