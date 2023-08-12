using Godot;
using System;

public partial class Laser : Area2D, IGameObject
{
    private Vector2 _speed;
    private Sprite2D _sprite;
    private CollisionShape2D _body;

    public static Laser Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/laser.tscn");
        return scene.Instantiate<Laser>();
    }

    public override void _Ready()
    {
        base._Ready();

        _speed = ToDirection(Rotation) * 1000;

        BodyEntered += OnCollide;
        AreaEntered += OnCollide;
    }

    public Vector2 Size => _sprite.Texture.GetSize();

    public override void _PhysicsProcess(double delta)
    {
        var deltaF = (float)delta;
        Position += _speed * deltaF;
    }

    private void OnCollide(Node2D body)
    {
        QueueFree();
    }

    public static Vector2 ToDirection(float angle) // TODO: Move to generic place
        => new Vector2(MathF.Sin(angle), -MathF.Cos(angle)).Normalized();
}
