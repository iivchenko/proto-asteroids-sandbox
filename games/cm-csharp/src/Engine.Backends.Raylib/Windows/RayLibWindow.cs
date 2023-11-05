using Engine.Game.Windows;

namespace Engine.Backends.Raylib.Windows;

public sealed class RayLibWindow : IWindow
{
    public bool IsOpen => !Raylib_cs.Raylib.WindowShouldClose();
}
