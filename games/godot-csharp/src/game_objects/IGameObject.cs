using Godot;

namespace ProtoAsteroidsGodotCSharp.game_objects
{
    public interface IGameObject
    {
        public Vector2 Position { get; set; }

        public Vector2 Size { get; }
    }
}
