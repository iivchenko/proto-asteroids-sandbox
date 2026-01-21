namespace Engine.EFS.Faces;

public interface IDrawableFace
{
    Sprite Sprite { get; set; }
    Vec Position { get; set; }
    Vec Origin { get; set; }
    Vec Scale { get; set; }
    Angle Rotation { get; set; }
    bool IsVisible { get; set; }
}