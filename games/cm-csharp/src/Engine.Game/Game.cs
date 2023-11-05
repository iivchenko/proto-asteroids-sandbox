using Engine.Game.Graphics;
using Engine.Game.Windows;
using Engine.Graphics;
using System.Diagnostics;
using System.Numerics;

namespace Engine.Game;

internal sealed class Game : IGame, IPainter
{
    private readonly Configuration _configuration;
    private readonly IWindowsSystem _windowsSystem;
    private readonly IGraphicsSystem _graphicsSystem;
    private readonly SceneBootstraper _bootstraper;

    // TODO: Think of useing Queue for optiomization
    private readonly List<SpriteDescriptor> _sprites;

    public Game(
        Configuration configuration,
        IWindowsSystem windowsSystem,
        IGraphicsSystem graphicsSystem, 
        SceneBootstraper bootstraper)
    {
        _configuration = configuration;
        _windowsSystem = windowsSystem;
        _graphicsSystem = graphicsSystem;
        _bootstraper = bootstraper;

        _sprites = new List<SpriteDescriptor>();
    }

    public void Run()
    {
        var window = _windowsSystem.Create(_configuration.WindowWidth, _configuration.WindowHeight, _configuration.WindowHeader);
        var scene = _bootstraper.Create();
        var stopwatch = new Stopwatch();
        var frame = (long) (1.0 / 60.0) * 1000;
        var delta = 0L;

        stopwatch.Start();

        while (window.IsOpen)
        {
            var start = stopwatch.ElapsedMilliseconds;
            
            scene.Update(delta / 1000.0f);
            _graphicsSystem.Draw(_sprites);
            _sprites.Clear();

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

    void IPainter.Draw(Sprite sprite, Vector2 position, Vector2 origin, Vector2 scale, float rotation, Color color)
    {
        _sprites.Add(new SpriteDescriptor(sprite, position, new Rectangle(), origin, scale, rotation, color));
    }

    void IPainter.Draw(Sprite sprite, Rectangle rectagle, Color color)
    {
        throw new NotImplementedException();
    }

    void IPainter.Draw(Sprite sprite, Rectangle destination, Rectangle source, Color color)
    {
        throw new NotImplementedException();
    }

    void IPainter.Draw(Sprite sprite, Vector2 position, Rectangle source, Vector2 origin, Vector2 scale, float rotation, Color color)
    {
        _sprites.Add(new SpriteDescriptor(sprite, position, source, origin, scale, rotation, color));
    }
}