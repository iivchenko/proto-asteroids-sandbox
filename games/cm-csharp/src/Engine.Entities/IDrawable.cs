namespace Engine.Entities;

public interface IDrawable
{
    int Position { get; set; } 

    void Draw(float delta);
}