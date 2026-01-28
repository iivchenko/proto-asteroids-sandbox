using Engine.Math;
using Engine.Utilities;
using Engine.Services;
using Engine;
using Game.Assets;

namespace Game.EFS.Entities;

public sealed class AsteroidBuilder(
    IAssetService<Sprite> spriteLoader, 
    IRandomService randomService,
    IViewService viewService)
{
    private const int TinyAsteroidMinSpeed = 400;
    private const int TinyAsteroidMaxSpeed = 500;
    private const int TinyAsteroidMinRotationSpeed = 25;
    private const int TinyAsteroidMaxRotationSpeed = 75;

    private const int SmallAsteroidMinSpeed = 200;
    private const int SmallAsteroidMaxSpeed = 300;
    private const int SmallAsteroidMinRotationSpeed = 25;
    private const int SmallAsteroidMaxRotationSpeed = 75;

    private const int MediumAsteroidMinSpeed = 100;
    private const int MediumAsteroidMaxSpeed = 200;
    private const int MediumAsteroidMinRotationSpeed = 15;
    private const int MediumAsteroidMaxRotationSpeed = 45;

    private const int BigAsteroidMinSpeed = 50;
    private const int BigAsteroidMaxSpeed = 100;
    private const int BigAsteroidMinRotationSpeed = 5;
    private const int BigAsteroidMaxRotationSpeed = 25;

    private readonly IAssetService<Sprite> _spriteLoader = spriteLoader;
    private readonly IRandomService _randomService = randomService;
    private readonly IViewService _viewService = viewService;

    private AsteroidType _type = AsteroidType.Tiny;
    private Vec _position = Vec.Zero;
    private Angle _direction = Angle.Zero;

    public AsteroidBuilder WithType(AsteroidType type)
    {
        _type = type;

        return this;
    }

    // TODO: I like this method, think of using it in the logic
    public AsteroidBuilder WithRandomType()
    {
        _type = 
            Enum
                .GetValues<AsteroidType>()
                .RandomPick();

        return this;
    }

    public AsteroidBuilder WithRandomPosition()
    {
        var view = _viewService.GetView();

        var (x, y) = _randomService.RandomInt(0, 1) switch
        {
            // Vertical
            0 => (_randomService.RandomInt(0, (int)view.Width), _randomService.RandomPick(0, (int)view.Width)),
            // Horizontal
            1 => (_randomService.RandomPick(0, (int)view.Height), _randomService.RandomInt(0, (int)view.Height)),
            _ => throw new NotImplementedException()
        };

        _position = new Vec(x, y);

        return this;
    }

    public AsteroidBuilder WithRandomDirection()
    {
        _direction = Angle.FromDegrees(_randomService.RandomInt(0, 360));
        return this;
    }

    public Asteroid Build()
    {
        Sprite sprite;
        int speedX;
        int speedY;
        Angle rotationSpeed;
        Vec velocity;

        var asteroidSprites = AssetStore.Sprites.Asteroids;

        switch (_type)
        {
            case AsteroidType.Tiny:
                sprite = _spriteLoader.Load(asteroidSprites.Tiny.AsteroidTiny01_png.Path);
                speedX = _randomService.RandomInt(TinyAsteroidMinSpeed, TinyAsteroidMaxSpeed);
                speedY = _randomService.RandomInt(TinyAsteroidMinSpeed, TinyAsteroidMaxSpeed);
                var tmpAngle1 = Angle.FromDegrees(_randomService.RandomInt(TinyAsteroidMinRotationSpeed, TinyAsteroidMaxRotationSpeed));

                rotationSpeed = tmpAngle1 * (_randomService.NextDouble() > 0.5 ? 1 : -1);
                velocity = _direction.ToVector() * new Vec(speedX, speedY);
                break;

            case AsteroidType.Small:
                sprite = _spriteLoader.Load(asteroidSprites.Small.AsteroidSmall01_png.Path);
                speedX = _randomService.RandomInt(SmallAsteroidMinSpeed, SmallAsteroidMaxSpeed);
                speedY = _randomService.RandomInt(SmallAsteroidMinSpeed, SmallAsteroidMaxSpeed);
                var tmpAngle2 = Angle.FromDegrees(_randomService.RandomInt(SmallAsteroidMinRotationSpeed, SmallAsteroidMaxRotationSpeed));

                rotationSpeed = tmpAngle2 * (_randomService.NextDouble() > 0.5 ? 1 : -1);
                velocity = _direction.ToVector() * new Vec(speedX, speedY);
                break;

            case AsteroidType.Medium:
                sprite = _spriteLoader.Load(asteroidSprites.Medium.AsteroidMedium01_png.Path);
                speedX = _randomService.RandomInt(MediumAsteroidMinSpeed, MediumAsteroidMaxSpeed);
                speedY = _randomService.RandomInt(MediumAsteroidMinSpeed, MediumAsteroidMaxSpeed);
                var tmpAngle3 = Angle.FromDegrees(_randomService.RandomInt(MediumAsteroidMinRotationSpeed, MediumAsteroidMaxRotationSpeed));

                rotationSpeed = tmpAngle3 * (_randomService.NextDouble() > 0.5 ? 1 : -1);
                velocity = _direction.ToVector() * new Vec(speedX, speedY);
                break;

            case AsteroidType.Big:
                sprite = _spriteLoader.Load(asteroidSprites.Big.AsteroidBig01_png.Path);
                speedX = _randomService.RandomInt(BigAsteroidMinSpeed, BigAsteroidMaxSpeed);
                speedY = _randomService.RandomInt(BigAsteroidMinSpeed, BigAsteroidMaxSpeed);
                var tmpAngle4 = Angle.FromDegrees(_randomService.RandomInt(BigAsteroidMinRotationSpeed, BigAsteroidMaxRotationSpeed));

                rotationSpeed = tmpAngle4 * (_randomService.NextDouble() > 0.5 ? 1 : -1);
                velocity = _direction.ToVector() * new Vec(speedX, speedY);
                break;
            default:
                throw new InvalidOperationException($"Unknown asteroid type {_type}!");
        }

        return new Asteroid(_type, sprite, velocity, Vec.One, rotationSpeed, _position);
    }
}