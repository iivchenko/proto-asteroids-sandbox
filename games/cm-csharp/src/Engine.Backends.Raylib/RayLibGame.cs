using Engine.Host;
using System.Diagnostics;

namespace Engine.Backends.Raylib;

public sealed class RayLibGame(
    GameConfiguration configuration,
    RayLibGame_GraphicsService graphicsSystem,
    SceneBootstraper bootstraper) : IGame
{
    private readonly GameConfiguration _configuration = configuration;
    private readonly RayLibGame_GraphicsService _graphicsSystem = graphicsSystem;
    private readonly SceneBootstraper _bootstraper = bootstraper;

    public void Run()
    {
        Raylib_cs.Raylib.InitWindow(_configuration.Window.Width, _configuration.Window.Height, _configuration.Window.Header);

        var scene = _bootstraper.Create();
        var stopwatch = new Stopwatch();
        var frame = (1.0f / 60.0f) * 1000.0f;
        var start = 0.0f;
        var end = 0.0f;
        var wait = 0.0f;
        var delta = 0.0f;

        stopwatch.Start();

        while (!Raylib_cs.Raylib.WindowShouldClose())
        {
            start = end;
            
            scene.Process(delta / 1000.0f);
            _graphicsSystem.Commit();
            
            end = stopwatch.ElapsedMilliseconds;
            delta = end - start;
            wait = frame - delta;
            if (wait > 0.0f)
            {
                Thread.Sleep((int)wait);
            }
        }

        stopwatch.Stop();
    }

    public void Dispose()
    {
        // TODO: Implement disposable
        //throw new NotImplementedException();
    }
}