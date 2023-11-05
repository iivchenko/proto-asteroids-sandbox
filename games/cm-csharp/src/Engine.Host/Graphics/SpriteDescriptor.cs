using Engine.Graphics;
using System.Numerics;

namespace Engine.Host.Graphics;

public sealed record SpriteDescriptor(
    Sprite Sprite, 
    Vector2 Position, 
    Rectangle Source, 
    Vector2 Origin, 
    Vector2 Scale, 
    float Rotation, 
    Color Color);