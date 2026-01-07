namespace Engine.Services;

public interface IRandomService
{
    int RandomInt(int start, int end);

    // TODO: It was a quick hack to fix build issues. Think proper on the interface of this service
    double NextDouble();
}