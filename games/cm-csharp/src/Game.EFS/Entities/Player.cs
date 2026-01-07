using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Game.EFS.Faces;

namespace Game.EFS.Entities;

public sealed class Player(
    Sprite sprite,
    Vec velocity,
    Vec scale,
    float rotationSpeed,
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

    private float _maxAngularAcceleration = 30.0f;
    private float _maxRotation = 290.0f;
    private float _maxAngularVelocity = 0.0f;
    private float _angularVelocity = 0.0f;
    private float _rotation = 0.0f;
    private Sprite _sprite = sprite;
    private Vec _position = position;
    private Vec _scale = scale;
    private float _rotationSpeed = rotationSpeed;
    private Vec _velocity = velocity;

    float IPlayerFace.Rotation { get => _rotation; set => _rotation = value; }
    float IPlayerFace.MaxAngularAcceleration { get => _maxAngularAcceleration; set => _maxAngularAcceleration = value; }
    float IPlayerFace.MaxRotation { get => _maxRotation; set => _maxRotation = value; }
    float IPlayerFace.MaxAngularVelocity { get => _maxAngularVelocity; set => _maxAngularVelocity = value; }
    float IPlayerFace.AngularVelocity { get => _angularVelocity; set => _angularVelocity = value; }

    Vec ICollidableFace.Position { get => _position; set => _position = value; }
    float ICollidableFace.Width { get => _sprite.Width; set { } }
    float ICollidableFace.Height { get => _sprite.Height; set { } }

    Sprite IDrawableFace.Sprite { get => _sprite; set => _sprite = value; }
    Vec IDrawableFace.Position { get => _position; set => _position = value; }
    Vec IDrawableFace.Origin { get => new(_sprite.Width / 2.0f, _sprite.Height / 2.0f); set { } }
    Vec IDrawableFace.Scale { get => _scale; set => _scale = value; }
    float IDrawableFace.Rotation { get => _rotation; set => _rotation = value; }

    Vec IMovableFace.Position { get => _position; set => _position = value; }
    Vec IMovableFace.Velocity { get => _velocity; set => _velocity = value; }
    float IMovableFace.Rotation { get => _rotation; set => _rotation = value; }
    float IMovableFace.RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
}
