using Engine.EFS;
using Engine.EFS.Systems;
using Engine.Services;
using Game.EFS.Entities;

namespace Game.EFS.Systems;

public sealed class AsteroidSpawnSystem : ISystem
{
    private const int MaxBigAsteroids = 5;
    private const int MaxMediumAsteroids = 10;
    private const int MaxSmallAsteroids = 15;
    private const int MaxTinyAsteroids = 20;

    private const float MaxCooldown = 5; // seconds

    private readonly IEntityBuilderFactory<AsteroidBuilder> _asteroidBuilderFactory;
    private readonly IRandomService _randomService;
    private readonly IDictionary<AsteroidType, int> _asteroids;

    private float _cooldown = MaxCooldown;

    public AsteroidSpawnSystem(
        IEntityBuilderFactory<AsteroidBuilder> asteroidBuilderFactory,
        IRandomService randomService)
    {
        _asteroidBuilderFactory = asteroidBuilderFactory;
        _randomService = randomService;
        _asteroids = new Dictionary<AsteroidType, int>
        {
            [AsteroidType.Big] = 0,
            [AsteroidType.Medium] = 0,
            [AsteroidType.Small] = 0,
            [AsteroidType.Tiny] = 0
        };
    }

    public IEnumerable<IWorldCommand> Process(IEnumerable<IEntity> faces, float delta)
    {
        if (_cooldown > 0)
        {
            _cooldown -= delta;
            yield break;
        }
        else
        {
            _cooldown = MaxCooldown;

            var asteroids =
                faces
                    .Where(face => face is Asteroid)
                    .Cast<Asteroid>();

            _asteroids[AsteroidType.Big] = MaxBigAsteroids - asteroids.Count(x => x.Type == AsteroidType.Big);
            _asteroids[AsteroidType.Medium] = MaxMediumAsteroids - asteroids.Count(x => x.Type == AsteroidType.Medium);
            _asteroids[AsteroidType.Small] = MaxSmallAsteroids - asteroids.Count(x => x.Type == AsteroidType.Small);
            _asteroids[AsteroidType.Tiny] = MaxTinyAsteroids - asteroids.Count(x => x.Type == AsteroidType.Tiny);

            var type = _randomService.RandomPick(_asteroids.Where(x => x.Value > 0).Select(x => x.Key));

            var builder = _asteroidBuilderFactory.Create();

            builder
                .WithType(type)
                .WithRandomPosition()
                .WithRandomDirection();

            yield return new AddEntityCommand(builder.Build());
        }
    }
}
