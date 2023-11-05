namespace Engine.Game;

public interface IGame : IDisposable
{
    // TODO: Think on replacing with async method. 
    // Can't do it now as RayLib failing with some
    // memory issues.
    void Run();
}
