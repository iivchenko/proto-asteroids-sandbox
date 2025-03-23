using Engine.Host;
using System.Diagnostics;

namespace Engine.Backends.Raylib;

public sealed class RayLibGame : IGame
{
    private readonly GameConfiguration _configuration;
    private readonly RayLibGraphicsSystem _graphicsSystem;
    private readonly SceneBootstraper _bootstraper;

    public RayLibGame(
        GameConfiguration configuration,
        RayLibGraphicsSystem graphicsSystem,
        SceneBootstraper bootstraper)
    {
        _configuration = configuration;
        _graphicsSystem = graphicsSystem;
        _bootstraper = bootstraper;
    }

    public void Run()
    {
        Raylib_cs.Raylib.InitWindow(_configuration.Window.Width, _configuration.Window.Height, _configuration.Window.Header);

        var scene = _bootstraper.Create();
        var stopwatch = new Stopwatch();
        var frame = (long) (1.0 / 60.0) * 1000;
        var delta = 0L;

        stopwatch.Start();

        while (!Raylib_cs.Raylib.WindowShouldClose())
        {
            var start = stopwatch.ElapsedMilliseconds;
            
            scene.Update(delta / 1000.0f);
            _graphicsSystem.Commit();

            delta = stopwatch.ElapsedMilliseconds - start;
            var wait = delta - frame;
            if (wait > 0)
            {
                Thread.Sleep((int)wait);
            }
        }

        stopwatch.Stop();
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}