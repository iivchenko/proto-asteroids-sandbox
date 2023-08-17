using Godot;
using System;

public partial class Blaster : Area2D
{
    private Vector2 _speed;
    private Sprite2D _sprite;
    private CollisionShape2D _body;

    public static Blaster Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/blaster.tscn");
        return scene.Instantiate<Blaster>();
    }

    public override void _Ready()
    {
        base._Ready();

        _speed = Direction * 1000;

        BodyEntered += OnCollide;
        AreaEntered += OnCollide;
    }

    public Vector2 Size => _sprite.Texture.GetSize();

    public Vector2 Direction { get; set; } = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        var deltaF = (float)delta;
        Position += _speed * deltaF;
    }

    private void OnCollide(Node2D body)
    {
        switch (body)
        {
            case Ufo _:
            case Laser _:
            case Blaster _:
                break;

            case PlayerShip player:
                QueueFree();
                player.Destroy();
                break;

            default:
                QueueFree();
                break;
        }       
    }

    public static Vector2 ToDirection(float angle) // TODO: Move to generic place
        => new Vector2(MathF.Sin(angle), -MathF.Cos(angle)).Normalized();
}
