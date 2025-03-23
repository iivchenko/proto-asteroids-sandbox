namespace Engine.EFS.Faces;

public interface IDrawable
{
    int Position { get; set; }

    void Draw(float delta);
}