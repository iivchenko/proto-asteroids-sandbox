using Engine;
using Engine.EFS;
using Engine.EFS.Faces;

namespace Game.EFS.Entities;

public sealed class Asteroid(
    Sprite sprite,
    Vec velocity,
    Vec scale,
    float rotationSpeed,
    Vec position) : 
        Entity, 
        IDrawableFace, 
        IMovableFace, 
        ICollidableFace
{
    private Sprite _sprite = sprite;
    private Vec _position = position;
    private Vec _scale = scale;
    private float _rotation = 0.0f;
    private float _rotationSpeed = rotationSpeed;
    private Vec _velocity = velocity;

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