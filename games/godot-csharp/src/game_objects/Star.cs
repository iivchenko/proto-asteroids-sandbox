using Godot;
using System;

public partial class Star : Node2D
{
	private Sprite2D _sprite;
	private Color _color;
	private double _time;
	private double _speed;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite");

		_color = new Color(
			(float)Random.Shared.NextDouble(),
			(float)Random.Shared.NextDouble(),
			(float)Random.Shared.NextDouble());
		_time = Random.Shared.Next(360);
		_speed = Random.Shared.NextDouble();

		var dir = DirAccess.Open("res://assets/sprites/stars/");
		var files = dir.GetFiles();
		var file = files[Random.Shared.Next(files.Length)];
		var path = "res://assets/sprites/stars/" + file;

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
