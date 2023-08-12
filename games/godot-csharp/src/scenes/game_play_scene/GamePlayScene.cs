using Godot;
using System;
using System.Linq;
using System.Threading.Tasks;

public partial class GamePlayScene : Node2D
{
    private int _nextAsteroid = 5000;
    private int _nextAsteroidDecrease = 60000;

    private int _score = 0;
    private int _live = 3;

    private Node2D _objectsLayer;
    private Node2D _skyLayer;
    private Node2D _player;
    private Vector2 _viewSize;

    private Label _scoreLabel;
    private Label _liveLabel;

    public override async void _Ready()
    {
        InitializeNodes();
        GenerateStarSky();
        InitializeInitialAsteroids();
        InitializePlayer();

        await Task.WhenAll(
            NextAsteroid(),
            NextAsteroidDecrease());
    }

    public override void _Process(double delta)
    {
        ScreenWrap();
        ScreenClean();
    }

    private void InitializeNodes()
    {
        _objectsLayer = GetNode<Node2D>("GameObjects");
        _skyLayer = GetNode<Node2D>("StarSkyLayer");

        _scoreLabel = GetNode<Label>("%ScoreLabel");
        _liveLabel = GetNode<Label>("%LiveLabel");
        _scoreLabel.Text = $"x {_score}";
        _liveLabel.Text = $"x {_live}";

        _viewSize = GetViewport().GetVisibleRect().Size;
    }

    private void GenerateStarSky()
    {
        var block = 96;

        for (var ix = 0; ix < _viewSize.X / block; ix += 1)
        {
            for (var iy = 0; iy < _viewSize.Y / block; iy += 1)
            {
                if (Random.Shared.Next(2) == 0)
                {
                    continue;
                }

                var scale = Random.Shared.Next(30, 150) / 100.0f;
                var star = Star.Instantiate();

                star.GlobalPosition = new Vector2(Random.Shared.Next(block) + ix * block, Random.Shared.Next(block) + iy * block);
                star.Rotation = Random.Shared.Next(314) / 100.0f;
                star.Scale = new Vector2(scale, scale);

                _skyLayer.AddChild(star);
            }
        }
    }

    private void InitializePlayer()
    {
        var instance = PlayerShip.Instantiate();
        var path = Asset.RandomAsset("res://assets/sprites/players_ships/");

        instance.Texture = GD.Load<Texture2D>(path);
        instance.GlobalPosition = new Vector2(_viewSize.X / 2.0f, _viewSize.Y / 2.0f);
        instance.Died += () =>
        {
            _live--;
            _liveLabel.Text = $"x {_live}";

            if (_live <= 0)
            {
                // TODO: Game Over
            }

            instance.GlobalPosition = new Vector2(_viewSize.X / 2.0f, _viewSize.Y / 2.0f);
            instance.Reset();
        };

        _objectsLayer.AddChild(instance);
    }

    private void InitializeInitialAsteroids()
    {
        RandomAsteroid();
        RandomAsteroid();
        RandomAsteroid();
        RandomAsteroid();
    }

    private void ScreenWrap()
    {
        _objectsLayer
            .OnlyChildren<IOnScreenGameObject>()
            .Iter(obj =>
            {
                obj.Position = new Vector2
                    (
                        obj.Position.X switch
                        {
                            float xl when xl + obj.Size.X / 2.0f < 0 => _viewSize.X + obj.Size.X / 2.0f,
                            float xl when xl - obj.Size.X / 2.0f > _viewSize.X => -obj.Size.X / 2.0f,
                            _ => obj.Position.X
                        },

                        obj.Position.Y switch
                        {
                            float yl when yl + obj.Size.Y / 2.0f < 0 => _viewSize.Y + obj.Size.Y / 2.0f,
                            float yl when yl - obj.Size.Y / 2.0f > _viewSize.Y => -obj.Size.Y / 2.0f,
                            _ => obj.Position.Y
                        }
                    );	
            });
    }

    private void ScreenClean()
    {
        _objectsLayer
            .OnlyChildren<IGameObject>()
            .Where(obj => obj is not IOnScreenGameObject)
            .Where(obj =>
                obj.Position.X < 0 ||
                obj.Position.Y < 0 ||
                obj.Position.X > _viewSize.X ||
                obj.Position.Y > _viewSize.Y
            )
            .Cast<Node>() // TODO: Think on avoid casts
            .Iter(obj =>
            {
                obj.QueueFree();
            });
    }

    private void RandomAsteroid()
    {
        static float Rand(float size)
        {
            return Random.Shared.Next((int)size + 1);
        }

        var asteroid = Asteroid.Instantiate();

        asteroid.GlobalPosition = Random.Shared.Next(4) switch
        {
            // UP
            0 => new Vector2(Rand(_viewSize.X), 0),

            // RIGHT
            1 => new Vector2(_viewSize.X, Rand(_viewSize.Y)),

            // DOWN
            2 => new Vector2(Rand(_viewSize.X), _viewSize.Y),

            // LEFT
            _ => new Vector2(0, Rand(_viewSize.Y))
        };

        asteroid.KilledByPlayer += (type) => {
            GD.Print("Shoty 1");
            switch (type)
            {
                case Asteroid.AsteroidType.Tiny:
                    _score += 4;
                    break;
                case Asteroid.AsteroidType.Small:
                    _score += 3;
                    break;
                case Asteroid.AsteroidType.Medium:
                    _score += 2;
                    break;
                case Asteroid.AsteroidType.Big:
                    _score += 1;
                    break;
            }

            _scoreLabel.Text = $"x {_score}";
        };

        _objectsLayer.AddChild(asteroid);
    }

    private async Task NextAsteroid()
    {
        await Task.Delay(_nextAsteroid);

        RandomAsteroid();

        await NextAsteroid();
    }

    private async Task NextAsteroidDecrease()
    {
        await Task.Delay(_nextAsteroidDecrease);

        if (_nextAsteroid > 1000) 
        {
            _nextAsteroid -= 1000;

            if (_nextAsteroid >= 1000)
            {
                await NextAsteroidDecrease();
            }
            else
            {
                _nextAsteroid = 1000;
            }
        }
    }
}
