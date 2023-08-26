using Godot;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class GamePlayScene : Node2D
{
    private int _score = 0;
    private int _live = 3;

    private Node2D _objectsLayer;
    private Node2D _skyLayer;
    private Node2D _player;
    private Vector2 _viewSize;

    private Label _scoreLabel;
    private Label _liveLabel;
    private Container _pauseScreen;
    private Button _pauseResumeBtn;
    private Button _pauseExitBtn;
    private Container _gameoverScreen;
    private Button _gameoverRestartBtn;
    private Button _gameoverExitBtn;

    private AudioStreamPlayer _music;

    [Export]
    public int NextAsteroid { get; set; } = 5000;

    [Export]
    public int NextAsteroidDecrease { get; set; } = 60000;


    [Export]
    public int NextUfo { get; set; } = 60000;

    [Export]
    public int NextUfoDecrease { get; set; } = 60000;

    public static PackedScene Load()
    {
        return (PackedScene)ResourceLoader.Load("res://scenes/game_play_scene/game_play_scene.tscn");
    }

    public override async void _Ready()
    {
        InitializeNodes();
        GenerateStarSky();
        InitializeInitialAsteroids();
        InitializePlayer();
        InitializeMusic();

        await Task.WhenAll(
            ProcessNextAsteroid(),
            ProcessNextAsteroidDecrease(),
            ProcessNextUfo(),
            ProcessNextUfoDecrease());
    }

    public override void _Process(double delta)
    {
        ScreenWrap();
        ScreenClean();
        InputProcess();
    }

    private void InitializeNodes()
    {
        _objectsLayer = GetNode<Node2D>("GameObjects");
        _skyLayer = GetNode<Node2D>("StarSkyLayer");

        _scoreLabel = GetNode<Label>("%ScoreLabel");
        _liveLabel = GetNode<Label>("%LiveLabel");
        _scoreLabel.Text = $"x {_score}";
        _liveLabel.Text = $"x {_live}";

        _pauseScreen = GetNode<Container>("Hud/GamePlayPauseComponent");
        _pauseResumeBtn = _pauseScreen.GetNode<Button>("%ResumeBtn");
        _pauseResumeBtn.Pressed += () =>
        {
            _pauseScreen.Visible = false;
            _pauseScreen.ProcessMode = ProcessModeEnum.Disabled;
            _objectsLayer.ProcessMode = ProcessModeEnum.Inherit;
        };

        _pauseExitBtn = _pauseScreen.GetNode<Button>("%ExitBtn");
        _pauseExitBtn.Pressed += () =>
        {
            GetTree().ChangeSceneToPacked(MainMenuScene.Load());
        };

        _gameoverScreen = GetNode<Container>("Hud/GamePlayOverComponent");
        _gameoverRestartBtn = _gameoverScreen.GetNode<Button>("%RestartBtn");
        _gameoverRestartBtn.Pressed += () =>
        {
            GetTree().ReloadCurrentScene();
        };

        _gameoverExitBtn = _gameoverScreen.GetNode<Button>("%ExitBtn");
        _gameoverExitBtn.Pressed += () =>
        {
            GetTree().ChangeSceneToPacked(MainMenuScene.Load());
        };

        _music = GetNode<AudioStreamPlayer>("Music");

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
                _objectsLayer.ProcessMode = ProcessModeEnum.Disabled;
                _gameoverScreen.GetNode<Label>("%ScoreLbl").Text = "Score: " + _score;
                _gameoverScreen.ProcessMode = ProcessModeEnum.Always;
                _gameoverScreen.Visible = true;
               
                return;
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

    private void InitializeMusic()
    {
        var path = Asset.RandomAsset("res://assets/music/game_music/");

        var audio = GD.Load<AudioStream>(path);

        _music.Stream = audio;

        _music.Play();
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

    private void InputProcess()
    {
        if (Input.IsActionJustPressed("ui_cancel") && _gameoverScreen.Visible == false)
        {
            if (_pauseScreen.ProcessMode == ProcessModeEnum.Disabled)
            {
                _pauseScreen.Visible = true;
                _pauseScreen.ProcessMode = ProcessModeEnum.Always;
                _objectsLayer.ProcessMode = ProcessModeEnum.Disabled;
            }
            else
            {
                _pauseScreen.Visible = false;
                _pauseScreen.ProcessMode = ProcessModeEnum.Disabled;
                _objectsLayer.ProcessMode = ProcessModeEnum.Inherit;
            }
        }
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

    private void RandomUfo()
    {
        static float Rand(float size)
        {
            return Random.Shared.Next((int)size + 1);
        }

        var ufo = Ufo.Instantiate();

        ufo.GlobalPosition = Random.Shared.Next(4) switch
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

        ufo.KilledByPlayer += () => {
            _score += 10;
            _scoreLabel.Text = $"x {_score}";
        };

        _objectsLayer.AddChild(ufo);
    }

    private async Task ProcessNextAsteroid()
    {
        await Task.Delay(NextAsteroid);

        RandomAsteroid();

        await ProcessNextAsteroid();
    }

    private async Task ProcessNextAsteroidDecrease()
    {
        await Task.Delay(NextAsteroidDecrease);

        if (NextAsteroid > 1000) 
        {
            NextAsteroid -= 1000;

            if (NextAsteroid >= 1000)
            {
                await ProcessNextAsteroidDecrease();
            }
            else
            {
                NextAsteroid = 1000;
            }
        }
    }

    private async Task ProcessNextUfo()
    {
        await Task.Delay(NextUfo);

        RandomUfo();

        await ProcessNextUfo();
    }

    private async Task ProcessNextUfoDecrease()
    {
        await Task.Delay(NextUfoDecrease);

        if (NextUfo > 1000)
        {
            NextUfo -= 1000;

            if (NextUfo >= 1000)
            {
                await ProcessNextUfoDecrease();
            }
            else
            {
                NextUfo = 1000;
            }
        }
    }
}
