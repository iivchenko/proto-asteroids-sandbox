using Godot;
using ProtoAsteroidsGodotCSharp.common;
using ProtoAsteroidsGodotCSharp.game_objects;
using System;

public partial class GamePlayScene : Node2D
{
	private Node2D _objectsLayer;
	private Node2D _skyLayer;
	private Node2D _player;
	private Vector2 _viewSize;

	public override void _Ready()
	{
		InitializeNodes();
		GenerateStarSky();
		InitializePlayer();
	}

	public override void _Process(double delta)
	{
		Wrap();
	}

	private void InitializeNodes()
	{
		_objectsLayer = GetNode<Node2D>("GameObjects");
		_skyLayer = GetNode<Node2D>("StarSkyLayer");

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

		_objectsLayer.AddChild(instance);
	}

	private void Wrap()
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
}
