namespace Engine.EFS.Faces;

public interface IDrawableFace
{
    Sprite Sprite { get; set; }
    Vec Position { get; set; }
    Vec Origin { get; set; }
    Vec Scale { get; set; }
    float Rotation { get; set; }
}