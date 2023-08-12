using System;
using System.Collections.Generic;
using Godot;

public partial class Asteroid : Area2D, IOnScreenGameObject
{
    private enum State
    {
        Live,
        Dies,
        Dead
    }

    private static readonly IDictionary<AsteroidType, AsteroidMeta> Metadata = new Dictionary<AsteroidType, AsteroidMeta>()
    {
        {
            AsteroidType.Tiny,
            new AsteroidMeta
            {
                MinSpeed = 100,
                MaxSpeed = 500,
                MinRotation = 70,
                MaxRotation = 90
            }
        },
        {
            AsteroidType.Small,
            new AsteroidMeta
            {
                MinSpeed = 80,
                MaxSpeed = 300,
                MinRotation = 50,
                MaxRotation = 70
            }
        },
        {
            AsteroidType.Medium,
            new AsteroidMeta
            {
                MinSpeed = 50,
                MaxSpeed = 150,
                MinRotation = 30,
                MaxRotation = 50
            }
        },
        {
            AsteroidType.Big,
            new AsteroidMeta
            {
                MinSpeed = 10,
                MaxSpeed = 100,
                MinRotation = 10,
                MaxRotation = 30
            }
        }
    };

    private State _state = State.Live;
    private Vector2 _speed;
    private float _rotationSpeed;
    private Sprite2D _sprite;
    private CollisionShape2D _body;
    private Node2D _death;

    public static Asteroid Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/asteroid.tscn");
        return scene.Instantiate<Asteroid>();
    }

    public override void _Ready()
    {
        base._Ready();

        InitializeAsteroid();

        BodyEntered += OnCollide;
        AreaEntered += OnCollide;
    }

    public Vector2 Size => _sprite.Texture.GetSize();

    private void InitializeAsteroid()
    {
        var type = Random.Shared.Pick(Enum.GetValues<AsteroidType>());
        var meta = Metadata[type];

        this
            .OnlyChildren<Sprite2D>()
            .Iter(node => node.Visible = false);

        this
            .OnlyChildren<CollisionShape2D>()
            .Iter(node => node.Disabled = true);

        _sprite = GetNode<Sprite2D>(type + "Sprite");
        _sprite.Visible = true;
        _body = GetNode<CollisionShape2D>(type + "Body");
        _body.Disabled = false;
        _death = GetNode<Node2D>("Death");

        _rotationSpeed = Random.Shared.Next(meta.MinRotation, meta.MaxRotation) / 100.0f;
        var speed = Random.Shared.Next(meta.MinSpeed, meta.MaxSpeed);
        _speed = Random.Shared.NextDirection() * speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        var deltaF = (float)delta;

        switch (_state)
        {
            case State.Live:
                ProcessLive(deltaF);
                break;
            case State.Dies:
                ProcessDies(deltaF);
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }

    private void ProcessLive(float delta)
    {
        Rotation += _rotationSpeed * delta;
        Position += _speed * delta;
    }

    private void ProcessDies(float _)
    {
        var isDead =
                  _death.GetNode<CpuParticles2D>("Particles1").Emitting == false &&
                  _death.GetNode<CpuParticles2D>("Particles2").Emitting == false &&
                  _death.GetNode<CpuParticles2D>("Particles3").Emitting == false;

        if (isDead)
        {
            _state = State.Dead;

            QueueFree();
        }
    }    

    private void OnCollide(Node2D body)
    {
        switch (body)
        {
            case PlayerShip player:
                player.Destroy();
                break;
        }

        _state = State.Dies;

        _sprite.Visible = false;
        _body.SetDeferred("Disabled", true);

        _death.GetNode<CpuParticles2D>("Particles1").Emitting = true;
        _death.GetNode<CpuParticles2D>("Particles2").Emitting = true;
        _death.GetNode<CpuParticles2D>("Particles3").Emitting = true;
    }

    public enum AsteroidType
    {
        Tiny,
        Small,
        Medium,
        Big
    }

    private sealed class AsteroidMeta
    {
        public int MinSpeed { get; init; }
        public int MaxSpeed { get; init; }
        public int MinRotation { get; init; }
        public int MaxRotation { get; init; }
    }
}
