namespace Engine.EFS.Faces;

public interface ICollidableFace
{
    Vec Position { get; set; }
    float Width { get; set; }
    float Height { get; set; }
}