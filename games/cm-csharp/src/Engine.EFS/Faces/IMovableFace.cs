namespace Engine.EFS.Faces;

public interface IMovableFace
{
    Vec Position { get; set; }
    Vec Velocity { get; set; }
    float Rotation { get; set; }
    float RotationSpeed { get; set; }
}
