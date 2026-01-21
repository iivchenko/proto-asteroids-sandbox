using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Game.EFS.Faces;

namespace Game.EFS.Entities;

public sealed class Player(
    Sprite sprite,
    Vec velocity,
    Vec scale,
    Angle rotationSpeed,
    Vec position) :
        Entity,
        IPlayerFace,
        IDrawableFace,
        IMovableFace,
        ICollidableFace
{
    // TODO: Implmenet Player movement with acceleration and deceleration
    //const float MaxSpeed = 600.0f;
    //const float Acceleration = 20.0f;

    private float _maxSpeed = 600.0f;
    private float _maxAcceleration = 20.0f;
    private Angle _maxAngularAcceleration = Angle.FromDegrees(30.0f);
    private Angle _maxRotation = Angle.FromDegrees(290.0f);
    private Angle _maxAngularVelocity = Angle.Zero;
    private Angle _angularVelocity = Angle.Zero;
    private Angle _rotation = Angle.Zero;
    private Sprite _sprite = sprite;
    private Vec _position = position;
    private Vec _scale = scale;
    private Angle _rotationSpeed = rotationSpeed;
    private Vec _velocity = velocity;
    private bool _isCollidable = true;
    private bool _isVisible = true;

    float IPlayerFace.MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }
    float IPlayerFace.MaxAcceleration { get => _maxAcceleration; set => _maxAcceleration = value; }
    Angle IPlayerFace.MaxAngularAcceleration { get => _maxAngularAcceleration; set => _maxAngularAcceleration = value; }
    Angle IPlayerFace.MaxRotation { get => _maxRotation; set => _maxRotation = value; }
    Angle IPlayerFace.MaxAngularVelocity { get => _maxAngularVelocity; set => _maxAngularVelocity = value; }
    Angle IPlayerFace.AngularVelocity { get => _angularVelocity; set => _angularVelocity = value; }

    Vec ICollidableFace.Position { get => _position; set => _position = value; }
    float ICollidableFace.Width { get => _sprite.Width; set { } }
    float ICollidableFace.Height { get => _sprite.Height; set { } }
    Vec ICollidableFace.Origin { get => new(_sprite.Width / 2.0f, _sprite.Height / 2.0f); set { } }
    Vec ICollidableFace.Scale { get => _scale; set => _scale = value; }
    Angle ICollidableFace.Rotation { get => _rotation; set => _rotation = value; }
    public bool IsCollidable { get => _isCollidable; set => _isCollidable = value; }
    public void OnCollide(ICollidableFace face)
    {
    }

    Sprite IDrawableFace.Sprite { get => _sprite; set => _sprite = value; }
    Vec IDrawableFace.Position { get => _position; set => _position = value; }
    Vec IDrawableFace.Origin { get => new(_sprite.Width / 2.0f, _sprite.Height / 2.0f); set { } }
    Vec IDrawableFace.Scale { get => _scale; set => _scale = value; }
    Angle IDrawableFace.Rotation { get => _rotation; set => _rotation = value; }
    public bool IsVisible { get => _isVisible; set => _isVisible = value; }

    Vec IMovableFace.Position { get => _position; set => _position = value; }
    Vec IMovableFace.LinearVelocity { get => _velocity; set => _velocity = value; }
    Angle IMovableFace.Rotation { get => _rotation; set => _rotation = value; }
    Angle IMovableFace.RotationVelocity { get => _rotationSpeed; set => _rotationSpeed = value; }
}
