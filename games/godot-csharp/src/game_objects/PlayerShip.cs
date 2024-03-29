using Godot;
using System;
using System.Threading.Tasks;

public partial class PlayerShip : CharacterBody2D, IOnScreenGameObject
{
    private enum State
    {
        Live,
        Dies,
        Dead
    }

    [Signal]
    public delegate void DiedEventHandler();

    private const float RotationSpeed = 10.0f;
    private const float MaxSpeed = 500.0f;
    private const float MaxAcceleration = 15.0f;

    private State _state = State.Live;
    private bool _canFire = true;

    private Sprite2D _sprite;
    private CollisionShape2D _body;
    private Marker2D _firePoint;
    private Node2D _death;
    private AudioStreamPlayer _deathSfx;
    private AudioStreamPlayer _lazerSfx;

    public Texture2D Texture
    {
        get => GetNode<Sprite2D>("Sprite").Texture;
        set => GetNode<Sprite2D>("Sprite").Texture = value;
    }

    public Vector2 Size
    {
        get => _sprite.Texture.GetSize();
    }

    public static PlayerShip Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/player_ship.tscn");
        return scene.Instantiate<PlayerShip>();
    }

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
        _body = GetNode<CollisionShape2D>("Body");
        _firePoint = GetNode<Marker2D>("FirePoint");
        _death = GetNode<Node2D>("Death");
        _deathSfx = GetNode<AudioStreamPlayer>("DeathSfx");
        _lazerSfx = GetNode<AudioStreamPlayer>("LaserSfx");
    }

    public async override void _Process(double delta)
    {
        var deltaF = (float)delta;
       
        switch (_state)
        {
            case State.Live:
                await ProcessLive(deltaF);
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

    public void Reset()
    {
        _state = State.Live;

        Velocity = Vector2.Zero;

        _sprite.Visible = true;
        _body.Disabled = false;
    }

    public void Destroy()
    {
        _state = State.Dies;

        Velocity = Vector2.Zero;

        _sprite.Visible = false;
        _body.SetDeferred("Disabled", true);

        _death.GetNode<CpuParticles2D>("Particles1").Emitting = true;
        _death.GetNode<CpuParticles2D>("Particles2").Emitting = true;
        _death.GetNode<CpuParticles2D>("Particles3").Emitting = true;

        _deathSfx.PitchScale = Random.Shared.Next(70, 120) / 100.0f;
        _deathSfx.Play();
    }

    public static Vector2 ToDirection(float angle)
        => new Vector2(MathF.Sin(angle), -MathF.Cos(angle)).Normalized();

    private async Task ProcessLive(float delta)
    {
        var turn = Input.GetActionStrength("player_turn_left") - Input.GetActionStrength("player_turn_right");

        if (turn > 0)
        {
            Rotation -= RotationSpeed * delta;
        }
        else if (turn < 0)
        {
            Rotation += RotationSpeed * delta;
        }

        if (Input.IsActionPressed("player_accelerate"))
        {
            var velocity = Velocity + ToDirection(Rotation) * MaxAcceleration;

            Velocity = velocity.Length() > MaxSpeed ? velocity.Normalized() * MaxSpeed : velocity;
        }

        if (Input.IsActionJustPressed("player_fire") && _canFire)
        {
            _canFire = false;
            var laser = Laser.Instantiate();

            laser.GlobalPosition = _firePoint.GlobalPosition;
            laser.Rotation = Rotation;

            GetParent().AddChild(laser);
            _lazerSfx.PitchScale = Random.Shared.Next(70, 120) / 100.0f;
            _lazerSfx.Play();
            await Task.Run(async () =>
            {
                await Task.Delay(500);
                _canFire = true;
            });
        }

        MoveAndSlide();
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

            EmitSignal(SignalName.Died);
        }
    }
}
