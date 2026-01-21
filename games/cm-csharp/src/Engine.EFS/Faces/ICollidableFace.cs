namespace Engine.EFS.Faces;

public interface ICollidableFace
{
    Vec Position { get; set; }
    Angle Rotation { get; set; }
    float Width { get; set; }
    float Height { get; set; }
    Vec Origin { get; set; }
    Vec Scale { get; set; }
    bool IsCollidable { get; set; }

    IWorldCommand OnCollide(ICollidableFace face);
}