using Engine;
using Engine.EFS;
using Engine.EFS.Faces;
using Game.EFS.Faces;

namespace Game.EFS.Entities;

public sealed class Asteroid(
    Sprite sprite,
    Vec velocity,
    Vec scale,
    Angle rotationSpeed,
    Vec position) : 
        Entity,
        IDrawableFace,
        IMovableFace, 
        ICollidableFace
{
    private Sprite _sprite = sprite;
    private Vec _position = position;
    private Vec _scale = scale;
    private Angle _rotation = Angle.Zero;
    private Angle _rotationSpeed = rotationSpeed;
    private Vec _velocity = velocity;
    private bool _isCollidable = true;
    private bool _isVisible = true;

    Vec ICollidableFace.Position { get => _position; set => _position = value; }
    float ICollidableFace.Width { get => _sprite.Width; set { } }
    float ICollidableFace.Height { get => _sprite.Height; set { } }
    Vec ICollidableFace.Origin { get => new(_sprite.Width / 2.0f, _sprite.Height / 2.0f); set { } }
    Vec ICollidableFace.Scale { get => _scale; set => _scale = value; }
    Angle ICollidableFace.Rotation { get => _rotation; set => _rotation = value; }
    public bool IsCollidable { get => _isCollidable; set => _isCollidable = value; }

    public IWorldCommand OnCollide(ICollidableFace face)
    {
        if (face is IPlayerFace || face is Projectile) // TODO: THink oin using face instead of entity for Projectile
        {
            _isCollidable = false;
            _isVisible = false;
            
            return new RemoveEntityCommand(this);
        }

        return EmptyEntityCommand.Empty;
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