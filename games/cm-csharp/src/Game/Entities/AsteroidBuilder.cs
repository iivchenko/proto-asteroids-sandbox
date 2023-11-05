using Engine.Assets;
using Engine.Graphics;
using System.Numerics;

namespace Game.Entities;

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

    private readonly IPainter _draw;
    private readonly IAssetProvider _assetProvider;

    private readonly Random _random;

    private AsteroidType _type = AsteroidType.Tiny;

    public AsteroidBuilder(
        IAssetProvider assetProvider,
        IPainter draw)
    {
        _assetProvider = assetProvider;       
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

    public Asteroid Build(Vector2 position, float direction)
    {
        Sprite sprite;
        int speedX;
        int speedY;
        int rotationSpeed;
        Vector2 velocity;

        switch (_type)
        {
            case AsteroidType.Tiny:
                sprite = _assetProvider.Load<Sprite>("Sprites/Asteroids/Tiny/AsteroidTiny01");
                speedX = _random.Next(TinyAsteroidMinSpeed, TinyAsteroidMaxSpeed);
                speedY = _random.Next(TinyAsteroidMinSpeed, TinyAsteroidMaxSpeed);
                rotationSpeed = _random.Next(TinyAsteroidMinRotationSpeed, TinyAsteroidMaxRotationSpeed).AsRadians() * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToDirection() * new Vector2(speedX, speedY);
                break;

            case AsteroidType.Small:
                sprite = _assetProvider.Load<Sprite>("Sprites/Asteroids/Small/AsteroidSmall01");
                speedX = _random.Next(SmallAsteroidMinSpeed, SmallAsteroidMaxSpeed);
                speedY = _random.Next(SmallAsteroidMinSpeed, SmallAsteroidMaxSpeed);
                rotationSpeed = _random.Next(SmallAsteroidMinRotationSpeed, SmallAsteroidMaxRotationSpeed).AsRadians() * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToDirection() * new Vector2(speedX, speedY);
                break;

            case AsteroidType.Medium:
                sprite = _assetProvider.Load<Sprite>("asteroid-big-01.png");
                speedX = _random.Next(MediumAsteroidMinSpeed, MediumAsteroidMaxSpeed);
                speedY = _random.Next(MediumAsteroidMinSpeed, MediumAsteroidMaxSpeed);
                rotationSpeed = _random.Next(MediumAsteroidMinRotationSpeed, MediumAsteroidMaxRotationSpeed).AsRadians() * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToDirection() * new Vector2(speedX, speedY);
                break;

            case AsteroidType.Big:
                sprite = _assetProvider.Load<Sprite>("Sprites/Asteroids/Big/AsteroidBig01");
                speedX = _random.Next(BigAsteroidMinSpeed, BigAsteroidMaxSpeed);
                speedY = _random.Next(BigAsteroidMinSpeed, BigAsteroidMaxSpeed);
                rotationSpeed = _random.Next(BigAsteroidMinRotationSpeed, BigAsteroidMaxRotationSpeed).AsRadians() * _random.NextDouble() > 0.5 ? 1 : -1;
                velocity = direction.ToDirection() * new Vector2(speedX, speedY);
                break;
            default:
                throw new InvalidOperationException($"Unknown asteroid type {_type}!");
        }
        var debri = _assetProvider.Load<Sprite>("Sprites/Asteroids/Tiny/AsteroidTiny01"); // TODO: Create own asteroid debri

        //var asteroid = new Asteroid(_draw, _player, _publisher, type, sprite, debri, _explosion, velocity, new Vector2(GameRoot.Scale), rotationSpeed)
        //{
        //    Position = position
        //};

        var asteroid = new Asteroid(_draw, _type, sprite, debri, velocity, new Vector2(GameRoot.Scale), rotationSpeed)
        {
            Position = position
        };

        //_collisionService.RegisterBody(asteroid, sprite);

        return asteroid;
    }
}