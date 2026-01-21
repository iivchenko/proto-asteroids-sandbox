namespace Engine.EFS.Faces;

public interface IMovableFace
{
    Vec Position { get; set; }
    Vec LinearVelocity { get; set; }
    Angle Rotation { get; set; }
    Angle RotationVelocity { get; set; }
}
