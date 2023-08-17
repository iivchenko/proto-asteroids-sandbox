using Godot;
using System;
using System.Threading.Tasks;

public partial class Ufo : Area2D, IOnScreenGameObject
{
    private const int Speed = 500;

    [Signal]
    public delegate void KilledByPlayerEventHandler();

    private enum State
    {
        Live,
        Dies,
        Dead
    }

    private State _state = State.Live;
    private Vector2 _speed;
    private Sprite2D _sprite;
    private Marker2D _firePoint;
    private CollisionShape2D _body;
    private Node2D _death;

    private bool _canFire = true;

    public static Ufo Instantiate()
    {
        var scene = (PackedScene)ResourceLoader.Load("res://game_objects/ufo.tscn");
        return scene.Instantiate<Ufo>();
    }

    public override void _Ready()
    {
        base._Ready();

        Initialize();

        BodyEntered += OnCollide;
        AreaEntered += OnCollide;
    }

    public Vector2 Size => _sprite.Texture.GetSize();

    private void Initialize()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
        _firePoint = GetNode<Marker2D>("FirePoint");
        _body = GetNode<CollisionShape2D>("Body");
        _death = GetNode<Node2D>("Death");

        _speed = Random.Shared.NextDirection() * Speed;
    }

    public override async void _PhysicsProcess(double delta)
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

    private async Task ProcessLive(float delta)
    {
        Position += _speed * delta;

        if (_canFire)
        {
            await Fire();
        }
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
        if (_state != State.Live)
        {
            return;
        }

        switch (body)
        {
            case PlayerShip player:
                player.Destroy();
                break;

            case Laser _:
                _state = State.Dies;

                _sprite.Visible = false;
                _body.SetDeferred("Disabled", true);

                _death.GetNode<CpuParticles2D>("Particles1").Emitting = true;
                _death.GetNode<CpuParticles2D>("Particles2").Emitting = true;
                _death.GetNode<CpuParticles2D>("Particles3").Emitting = true;

                EmitSignal(SignalName.KilledByPlayer);

                break;

            case Ufo _:
                _state = State.Dies;

                _sprite.Visible = false;
                _body.SetDeferred("Disabled", true);

                _death.GetNode<CpuParticles2D>("Particles1").Emitting = true;
                _death.GetNode<CpuParticles2D>("Particles2").Emitting = true;
                _death.GetNode<CpuParticles2D>("Particles3").Emitting = true;

                break;
        }
    }

    private async Task Fire()
    {
        _canFire = false;
        var blaster = Blaster.Instantiate();
        var player = GetParent().GetNode<PlayerShip>("PlayerShip");
        var direcrtion = (player.GlobalPosition - GlobalPosition).Normalized();

        blaster.GlobalPosition = _firePoint.GlobalPosition;
        blaster.Direction = (player.GlobalPosition - GlobalPosition).Normalized();
        blaster.Rotation = direcrtion.Angle();

        GD.Print(blaster.Direction);

        GetParent().AddChild(blaster);

        await Task.Run(async () =>
        {
            await Task.Delay(5000);
            _canFire = true;
        });
    }
}
