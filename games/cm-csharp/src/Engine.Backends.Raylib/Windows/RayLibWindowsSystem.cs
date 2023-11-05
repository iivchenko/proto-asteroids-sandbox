using Engine.Game.Windows;

namespace Engine.Backends.Raylib.Windows;

public sealed class RayLibWindowsSystem : IWindowsSystem
{
    public IWindow Create(int width, int height, string header)
    {
        var window = new RayLibWindow();
        Raylib_cs.Raylib.InitWindow(width, height, header);

        return window;
    }
}
