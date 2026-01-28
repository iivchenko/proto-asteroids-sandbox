using Engine;
using Game.EFS.Entities;

namespace Game.EFS.Faces;

public interface IPlayerFace
{
    PlayerState State { get; set; }
    float MaxSpeed { get; set; }
    float MaxAcceleration { get; set; }
    Angle MaxAngularAcceleration { get; set; }
    Angle MaxAngularVelocity { get; set; }
    Angle MaxRotation { get; set; }
    Angle AngularVelocity { get; set; }
    float LaserCooldown { get; set; }
}
