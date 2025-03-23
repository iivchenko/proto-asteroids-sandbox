namespace Engine.EIS;

public interface IDrawable
{
    int Position { get; set; } 

    void Draw(float delta);
}