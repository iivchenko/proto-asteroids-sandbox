using Engine.Math;
using Engine.Utilities;
using Engine.Services;
using Engine;
using Game.Assets;

namespace Game.EFS.Entities;

public sealed class AsteroidBuilder
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

    private readonly IGraphicsService _draw;
    private readonly IAssetService<Sprite> _spriteLoader;

    private readonly Random _random;

    private AsteroidType _type = AsteroidType.Tiny;

    public AsteroidBuilder(
        IAssetService<Sprite> spriteLoader,
        IGraphicsService draw)
    {
        _spriteLoader = spriteLoader;       
        _draw = draw;
        _random = new Random();
    }

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

    public Asteroid Build(Vec position, Angle direction)
    {
        Sprite sprite;
        int speedX;
        int speedY;
        int rotationSpeed;
        Vec velocity;

        var asteroidSprites = AssetStore.Sprites.Asteroids;

        switch (_type)
        {
            case AsteroidType.Tiny:
                sprite = _spriteLoader.Load(asteroidSprites.Tiny.AsteroidTiny01_png.Path);
                speedX = _random.Next(TinyAsteroidMinSpeed, TinyAsteroidMaxSpeed);
                speedY = _random.Next(TinyAsteroidMinSpeed, TinyAsteroidMaxSpeed);
                var tmpAngle1 = new Angle
                    (
                        _random.Next(TinyAsteroidMinRotationSpeed, TinyAsteroidMaxRotationSpeed),
                        AngleType.Degrees
                    );

                rotationSpeed = tmpAngle1.ToRadians().Value * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToVector() * new Vec(speedX, speedY);
                break;

            case AsteroidType.Small:
                sprite = _spriteLoader.Load(asteroidSprites.Small.AsteroidSmall01_png.Path);
                speedX = _random.Next(SmallAsteroidMinSpeed, SmallAsteroidMaxSpeed);
                speedY = _random.Next(SmallAsteroidMinSpeed, SmallAsteroidMaxSpeed);
                var tmpAngle2 = new Angle
                (
                    _random.Next(SmallAsteroidMinRotationSpeed, SmallAsteroidMaxRotationSpeed),
                    AngleType.Degrees
                );

                rotationSpeed = tmpAngle2.ToRadians().Value * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToVector() * new Vec(speedX, speedY);
                break;

            case AsteroidType.Medium:
                sprite = _spriteLoader.Load(asteroidSprites.Medium.AsteroidMedium01_png.Path);
                speedX = _random.Next(MediumAsteroidMinSpeed, MediumAsteroidMaxSpeed);
                speedY = _random.Next(MediumAsteroidMinSpeed, MediumAsteroidMaxSpeed);
                var tmpAngle3 = new Angle
                (
                   _random.Next(MediumAsteroidMinRotationSpeed, MediumAsteroidMaxRotationSpeed),
                    AngleType.Degrees
                );
                rotationSpeed = tmpAngle3.ToRadians().Value * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToVector() * new Vec(speedX, speedY);
                break;

            case AsteroidType.Big:
                sprite = _spriteLoader.Load(asteroidSprites.Big.AsteroidBig01_png.Path);
                speedX = _random.Next(BigAsteroidMinSpeed, BigAsteroidMaxSpeed);
                speedY = _random.Next(BigAsteroidMinSpeed, BigAsteroidMaxSpeed);
                var tmpAngle4 = new Angle
                (
                    _random.Next(BigAsteroidMinRotationSpeed, BigAsteroidMaxRotationSpeed),
                    AngleType.Degrees
                );
                rotationSpeed = tmpAngle4.ToRadians().Value * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToVector() * new Vec(speedX, speedY);
                break;
            default:
                throw new InvalidOperationException($"Unknown asteroid type {_type}!");
        }
        var debri = _spriteLoader.Load(asteroidSprites.Tiny.AsteroidTiny01_png.Path); // TODO: Create own asteroid debri

        //var asteroid = new Asteroid(_draw, _player, _publisher, type, sprite, debri, _explosion, velocity, new Vector2(GameRoot.Scale), rotationSpeed)
        //{
        //    Position = position
        //};

        var asteroid = new Asteroid(_draw, _type, sprite, debri, velocity, Vec.One, rotationSpeed)
        {
            Position = position
        };

        //_collisionService.RegisterBody(asteroid, sprite);

        return asteroid;
    }
}