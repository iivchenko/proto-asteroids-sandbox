namespace Engine.Game.Windows;

public interface IWindowsSystem
{
    public IWindow Create(int width, int height, string header);
}
