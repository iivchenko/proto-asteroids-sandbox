using Godot;
using System;

public partial class GamePlayScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GenerateStarSky();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GenerateStarSky()
	{
		GD.Print("Start");

		var block = 96;
		var size = GetViewport().GetVisibleRect().Size;
		var sky = GetNode<Node2D>("StarSkyLayer");

		for (var ix = 0; ix < size.X  / block; ix += 1)
		{
			for (var iy = 0; iy < size.Y / block; iy += 1)
			{
				if (Random.Shared.Next(2) == 0)
				{
					continue;
				}

				var scale = Random.Shared.Next(30, 150) / 100.0f;
				var scene = (PackedScene)ResourceLoader.Load("res://game_objects/star.tscn");
				var star = scene.Instantiate<Node2D>();

				star.GlobalPosition = new Vector2(Random.Shared.Next(block) + ix * block, Random.Shared.Next(block) + iy * block);
				star.Rotation = Random.Shared.Next(314) / 100.0f;
				star.Scale = new Vector2(scale, scale);

				sky.AddChild(star);

				GD.Print("Generated");
				

			}
		}

	}
}
