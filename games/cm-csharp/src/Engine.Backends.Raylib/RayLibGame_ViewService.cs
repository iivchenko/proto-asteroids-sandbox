using Engine.Services;
using Engine.Services.Keyboard;

namespace Engine.Backends.Raylib;

public sealed class RayLibGame_ViewService : IViewService
{
    public View GetView()
    {
        return new View(Raylib_cs.Raylib.GetRenderWidth(), Raylib_cs.Raylib.GetRenderHeight());
    }
}

public sealed class RayLibGame_KeyboardService : IKeyboardService
{
    public bool IsKeyDown(Keys key)
    {
        var raylibKey = key switch
        {
            Keys.ArrowLeft => Raylib_cs.KeyboardKey.Left,
            Keys.ArrowUp => Raylib_cs.KeyboardKey.Up,
            Keys.ArrowRight => Raylib_cs.KeyboardKey.Right,
            Keys.ArrowDown => Raylib_cs.KeyboardKey.Down,
            Keys.Space => Raylib_cs.KeyboardKey.Space,
            _ => throw new ArgumentOutOfRangeException(nameof(key), $"Not expected key value: {key}") // TODO: Finishe all the other mappings
        };

        return Raylib_cs.Raylib.IsKeyDown(raylibKey);
    }
}