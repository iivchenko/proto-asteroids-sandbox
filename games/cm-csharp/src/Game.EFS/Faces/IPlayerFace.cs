namespace Game.EFS.Faces;

public interface IPlayerFace
{
    float MaxAngularAcceleration { get; set; }
    float MaxAngularVelocity { get; set; }
    float MaxRotation { get; set; }
    float AngularVelocity { get; set; }
    float Rotation { get; set; }
}
