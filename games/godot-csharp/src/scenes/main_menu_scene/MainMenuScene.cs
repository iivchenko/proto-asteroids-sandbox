using Godot;
using System;

public partial class MainMenuScene : Container
{
    private Node2D _stars;
    private Vector2 _viewSize;

    private Button _startBtn;
    private Button _exitBtn;

    public override void _Ready()
    {
        _stars = GetNode<Node2D>("Stars");
        _viewSize = GetViewport().GetVisibleRect().Size;

        _startBtn = GetNode<Control>("MainMenu").GetNode<Button>("%StartBtn");
        _startBtn.GrabFocus();
        _startBtn.Pressed += () =>
        {
            GetTree().ChangeSceneToPacked(GamePlayScene.Load());
        };

        _exitBtn = GetNode<Control>("MainMenu").GetNode<Button>("%ExitBtn");
        _exitBtn.Pressed += () =>
        {
            GetTree().Quit();
        };

        GenerateStarSky();
    }

    private void GenerateStarSky()
    {
        var block = 96;

        for (var ix = 0; ix < _viewSize.X / block; ix += 1)
        {
            for (var iy = 0; iy < _viewSize.Y / block; iy += 1)
            {
                if (Random.Shared.Next(2) == 0)
                {
                    continue;
                }

                var scale = Random.Shared.Next(30, 150) / 100.0f;
                var star = Star.Instantiate();

                star.GlobalPosition = new Vector2(Random.Shared.Next(block) + ix * block, Random.Shared.Next(block) + iy * block);
                star.Rotation = Random.Shared.Next(314) / 100.0f;
                star.Scale = new Vector2(scale, scale);

                _stars.AddChild(star);
            }
        }
    }
}
