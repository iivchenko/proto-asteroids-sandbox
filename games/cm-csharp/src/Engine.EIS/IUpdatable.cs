namespace Engine.EIS;

public interface IUpdatable
{
    int Position { get; set; }
    void Update(float delta);
}
