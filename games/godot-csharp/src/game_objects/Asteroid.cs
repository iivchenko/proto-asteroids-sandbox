using System;
using System.Collections.Generic;
using Godot;

public partial class Asteroid : Area2D, IOnScreenGameObject
{
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

    private Vector2 _speed;
    private float _rotationSpeed;
    private Sprite2D _sprite;

    public override void _Ready()
    {
        base._Ready();

        InitializeAsteroid();
    }

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
        GetNode<CollisionShape2D>(type + "Body").Disabled = false;

        _rotationSpeed = Random.Shared.Next(meta.MinRotation, meta.MaxRotation) / 100.0f;
        var speed = Random.Shared.Next(meta.MinSpeed, meta.MaxSpeed);
        _speed = Random.Shared.NextDirection() * speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        var deltaF = (float)delta;

        Rotation += _rotationSpeed * deltaF;
        Position += _speed * deltaF;
    }

    public Vector2 Size => _sprite.Texture.GetSize();

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
