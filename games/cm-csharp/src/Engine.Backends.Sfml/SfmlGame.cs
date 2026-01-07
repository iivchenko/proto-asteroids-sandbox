using Engine.Host;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace Engine.Backends.Sfml;

public sealed partial class SfmlGame : IGame
{
    private readonly GameConfiguration _configuration;
    private readonly SceneBootstraper _bootstraper;
    private readonly RenderWindow _window;

    public SfmlGame(
        GameConfiguration configuration,
        SceneBootstraper bootstraper)
    {
        _configuration = configuration;
        _bootstraper = bootstraper; 

        _window = new RenderWindow(
            new VideoMode(
                (uint)_configuration.Window.Width, 
                (uint)_configuration.Window.Height), 
            _configuration.Window.Header);

        _window.Closed += (_, _) => _window.Close();
    }

    public void Run()
    {
        var scene = _bootstraper.Create();
        var stopwatch = new Stopwatch();
        var delta = 0L;

        stopwatch.Start();

        while (_window.IsOpen)
        {
            var start = stopwatch.ElapsedMilliseconds;

            scene.Process(delta / 1000.0f);

            _window.DispatchEvents();

            Display(); 

            delta = stopwatch.ElapsedMilliseconds - start;
        }
        
        stopwatch.Stop();
    }

    public void Dispose()
    {
        // TODO: Implement
    }
}
