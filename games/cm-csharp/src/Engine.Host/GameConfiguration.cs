namespace Engine.Host;

public sealed class GameConfiguration
{
    public WindowConfiguraiton Window { get; set; } = new WindowConfiguraiton();
}

public sealed class WindowConfiguraiton
{
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public string Header { get; set; } = "The Game";
}