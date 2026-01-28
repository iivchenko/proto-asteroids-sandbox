using Engine;
using Engine.Services;
using Game.Assets;

namespace Game.EFS.Entities;

public sealed class ProjectileBuilder(IAssetService<Sprite> spriteLoader)
{
    private const float Speed = 1200.0f;

    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;

    private Vec _position = Vec.Zero;
    private Vec _direction = Vec.Zero;
    private Angle _rotation = Angle.Zero;

    public ProjectileBuilder WithPosition(Vec position)
    {
        _position = position;

        return this;
    }

    public ProjectileBuilder WithRotation(Angle rotation)
    {
        _rotation = rotation;

        return this;
    }

    public ProjectileBuilder WithDirection(Vec direction)
    {
        _direction = direction;

        return this;
    }

    public Projectile Build()
    {
        var sprite = _spriteLoader.Load(AssetStore.Sprites.Lasers.Laser01_png.Path);
        var velocity = _direction * Speed;

        return new Projectile(sprite, velocity, Vec.One, _rotation, _position);
    }
}
