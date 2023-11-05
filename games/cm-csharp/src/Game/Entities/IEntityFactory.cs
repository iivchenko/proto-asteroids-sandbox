using System.Numerics;

namespace Game.Entities;

public interface IEntityFactory
{
    Asteroid CreateAsteroid(AsteroidType type, Vector2 position, float direction);
    //Ship CreateShip(Vector2 position);
    //Ufo CreateUfo(Vector2 position, float direction);
}
