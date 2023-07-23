using Godot;
using System;

public partial class PlayerShip : CharacterBody2D, IOnScreenGameObject
{
    private const float RotationSpeed = 10.0f;
    private const float MaxSpeed = 500.0f;
    private const float MaxAcceleration = 15.0f;

    private Sprite2D _sprite;

    public static PlayerShip Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/player_ship.tscn");
        return scene.Instantiate<PlayerShip>();
    }

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
    }

    public Texture2D Texture
    {
        get => GetNode<Sprite2D>("Sprite").Texture;
        set => GetNode<Sprite2D>("Sprite").Texture = value;
    }

    public Vector2 Size 
    {
        get => _sprite.Texture.GetSize();
    }

    public override void _Process(double delta)
    {
        var deltaF = (float)delta;
        var turn = Input.GetActionStrength("player_turn_left") - Input.GetActionStrength("player_turn_right");

        if (turn > 0)
        {
            Rotation -= RotationSpeed * deltaF;
        }
        else if (turn < 0)
        {
            Rotation += RotationSpeed * deltaF;
        }

        if (Input.IsActionPressed("player_accelerate"))
        {
            var velocity = Velocity + ToDirection(Rotation) * MaxAcceleration;

            Velocity = velocity.Length() > MaxSpeed ? velocity.Normalized() * MaxSpeed : velocity;            
        }
      
        if (Input.IsActionJustPressed("player_fire"))
        {
            // Fire code
        }

        MoveAndSlide();
    }

    public static Vector2 ToDirection(float angle)
        => new Vector2(MathF.Sin(angle), -MathF.Cos(angle)).Normalized();
}
