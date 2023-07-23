using Godot;
using System;

public partial class Star : Node2D
{
    private Sprite2D _sprite;
    private Color _color;
    private double _time;
    private double _speed;

    public static Star Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/star.tscn");
        return scene.Instantiate<Star>();
    }

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite");

        _color = new Color(
            (float)Random.Shared.NextDouble(),
            (float)Random.Shared.NextDouble(),
            (float)Random.Shared.NextDouble());
        _time = Random.Shared.Next(360);
        _speed = Random.Shared.NextDouble();

        var path = Asset.RandomAsset("res://assets/sprites/stars/");

        _sprite.Texture = GD.Load<Texture2D>(path);
        _sprite.Modulate = _color;
    }

    public override void _Process(double delta)
    {
        var modulation = (float)Math.Abs(Math.Sin(_time));

        _sprite.Modulate = new Color(
            _color.R * modulation,
            _color.G * modulation,
            _color.B * modulation);

        _time += delta * _speed;
    }
}
