using Engine;

namespace Game.EFS.Faces;

public interface IPlayerFace
{
    float MaxSpeed { get; set; }
    float MaxAcceleration { get; set; }
    Angle MaxAngularAcceleration { get; set; }
    Angle MaxAngularVelocity { get; set; }
    Angle MaxRotation { get; set; }
    Angle AngularVelocity { get; set; }
}
